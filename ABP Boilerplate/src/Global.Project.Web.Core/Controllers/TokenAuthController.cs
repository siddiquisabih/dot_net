using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using Abp.Domain.Repositories;
using Global.Project.Authorization;
using Global.Project.Authorization.Users;
using Global.Project.Model;
using Global.Project.MultiTenancy;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Global.Project.Auditor;
using Global.Project.Models.TokenAuth;
using Global.Project.Authentication.JwtBearer;
using Global.Project.Authentication.External;
using Global.Project.Emails;

namespace Global.Project.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : ProjectControllerBase
    {
        private readonly IRepository<User, long> _userRepo;
        private readonly IRepository<UserLogin, long> _userLoginRepo;
        private readonly IRepository<Tenant> _tentRepo;
        private readonly IRepository<AuditType> _auditTypeRepository;
        private readonly IRepository<AuditLog> _auditLogRepository;
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IAuditorAppService _auditorAppService;
        private readonly UserManager _userManager;
        private readonly IDistributedCache _cache;
        private readonly ProjectEmailManager _emailManager;



        public TokenAuthController(
            IRepository<AuditType> auditTypeRepository,
            IRepository<AuditLog> auditLogRepository,
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager,
            IAuditorAppService auditorAppService,
            UserManager userManager,
            IDistributedCache cache,
            IRepository<User, long> userRepo,
            IRepository<UserLogin, long> userLoginRepo,
            IRepository<Tenant> tentRepo,
            ProjectEmailManager emailManager
            )
        {
            _auditLogRepository = auditLogRepository;
            _auditTypeRepository = auditTypeRepository;
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
            _auditorAppService = auditorAppService;
            _userManager = userManager;
            _cache = cache;
            _userRepo = userRepo;
            _userLoginRepo = userLoginRepo;
            _tentRepo = tentRepo;
            _emailManager = emailManager;
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {

            try
            {
                var loginResult = await GetLoginResultAsync(
                           model.UserNameOrEmailAddress,
                           model.Password,
                           tenancyName: model.TenancyName
                //GetTenancyNameOrNull()
                );

                var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

                if (loginResult.Result.ToString() == "Success")
                {
                    _auditorAppService.InsertAuditLog("login", Convert.ToInt32(loginResult.User.Id));
                }

                string refreshToken = GenerateRefreshToken();
                await CacheRefreshToken(refreshToken: refreshToken, userId: Convert.ToInt32(loginResult.User.Id));

                return new AuthenticateResultModel
                {
                    AccessToken = accessToken,
                    EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                    ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                    UserId = loginResult.User.Id,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Invalid Email or Password");
            }

        }

        [HttpGet]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        }

        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = await GetExternalUserInfo(model);

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true
                            };
                        }

                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }

                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity)),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        private async Task<User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.EmailAddress,
                Authorization.Users.User.CreateRandomPassword(),
                true
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private async Task<bool> CacheRefreshToken(string refreshToken, int userId)
        {

            await _cache.SetStringAsync($"Refresh_token_{userId}", refreshToken, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_configuration.RefreshTokenExpiration)
            });

            return true;
        }

        private async Task<string> GetRefreshTokenFromCache(int userId)
        {
            string cache = await _cache.GetStringAsync($"Refresh_token_{userId}");
            return cache;
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });
            return claims;
        }

        private string GetEncryptedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginOtpResponseModel> LoginWithEmail([FromBody] LoginWithEmailModel model)
        {

            var user = _userRepo.GetAll().IgnoreQueryFilters().Where(x => x.EmailAddress == model.EmailAddress && x.IsDeleted == false)
                  .SingleOrDefault() ?? throw new UserFriendlyException($"{L("User_not_found_with_email")} {model.EmailAddress}");

            if (user.IsActive == false)
            {
                throw new UserFriendlyException(L("Account_is_not_active_contact_support"));
            }

            try
            {
                if (user != null)
                {
                    Random random = new();
                    string otp = random.Next(100000, 999999).ToString();

                    await _cache.SetStringAsync($"OTP_{model.EmailAddress}", otp, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });

                    var task = Task.Run(async () =>
                    {
                        await _emailManager.SendLoginOtpEmail(model.EmailAddress, otp);
                    });

                    return new LoginOtpResponseModel
                    {
                        Success = true,
                        Message = L("OTP_has_been_sent_to_your_email"),
                        TempOtp = otp
                    };
                }
                else
                {
                    throw new UserFriendlyException(L("Email_not_found"));
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(L("error", ex.Message));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<AuthenticateResultModel> VerifyOTP([FromBody] VerifyOtpModel model)
        {
            try
            {
                string cachedOTP = await _cache.GetStringAsync($"OTP_{model.EmailAddress}");

                if (string.IsNullOrEmpty(cachedOTP) || cachedOTP != model.OTP)
                {
                    throw new UserFriendlyException(L("Invalid_OTP_or_OTP_expired"));
                }

                var user = _userRepo.GetAll().IgnoreQueryFilters().Where(x => x.EmailAddress == model.EmailAddress && x.IsDeleted == false).FirstOrDefault();
                var existingLogin = _userLoginRepo.GetAll().IgnoreQueryFilters().Where(x => x.ProviderKey == model.EmailAddress).FirstOrDefault();

                if (existingLogin == null)
                {
                    if (user.Logins == null)
                    {
                        user.Logins = new List<UserLogin>();
                    }

                    user.Logins.Add(new UserLogin
                    {
                        LoginProvider = "OTP",
                        ProviderKey = user.EmailAddress,
                        TenantId = user.TenantId
                    });
                    await CurrentUnitOfWork.SaveChangesAsync();
                }

                var tenent = _tentRepo.GetAll().IgnoreQueryFilters().Where(x => x.Id == user.TenantId).FirstOrDefault();
                var loginResult = await _logInManager.LoginAsync(new UserLoginInfo("OTP", user.EmailAddress, "OTP Authentication"), tenent.TenancyName);


                if (loginResult.Result != AbpLoginResultType.Success)
                {
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                        loginResult.Result,
                        model.EmailAddress,
                        GetTenancyNameOrNull()
                    );
                }

                var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                await _cache.RemoveAsync($"OTP_{model.EmailAddress}");
                string refreshToken = GenerateRefreshToken();
                await CacheRefreshToken(refreshToken: refreshToken, userId: Convert.ToInt32(loginResult.User.Id));

                return new AuthenticateResultModel
                {
                    AccessToken = accessToken,
                    EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                    ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                    UserId = loginResult.User.Id,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<AuthenticateResultModel> RefreshToken([FromBody] RefreshTokenRequest requestData)
        {


            if (requestData.UserId <= 0)
            {
                throw new UserFriendlyException(L("User_Id_is_required"));
            }

            if (string.IsNullOrEmpty(requestData.RefreshToken))
            {
                throw new UserFriendlyException(L("Refresh_Token_is_required"));
            }

            try
            {
                string cacheToken = await GetRefreshTokenFromCache(requestData.UserId);

                if (string.IsNullOrEmpty(cacheToken) || cacheToken != requestData.RefreshToken)
                {
                    throw new UserFriendlyException(L("Invalid_Refresh_Token"));
                }

                var user = _userRepo.GetAll()
                    .IgnoreQueryFilters()
                    .Where(x => x.Id == requestData.UserId)
                    .FirstOrDefault() ?? throw new UserFriendlyException(L("User_not_with_provided_User_Id"));

                var tenent = _tentRepo.GetAll().IgnoreQueryFilters().Where(x => x.Id == user.TenantId).FirstOrDefault();
                var dummyLogin = await _logInManager.LoginAsync(new UserLoginInfo("OTP", user.EmailAddress, "OTP Authentication"), tenent.TenancyName);

                if (dummyLogin.Result != AbpLoginResultType.Success)
                {
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                        dummyLogin.Result,
                        user.EmailAddress,
                        GetTenancyNameOrNull()
                    );
                }

                var accessToken = CreateAccessToken(CreateJwtClaims(dummyLogin.Identity));
                string refreshToken = GenerateRefreshToken();
                await CacheRefreshToken(refreshToken: refreshToken, userId: Convert.ToInt32(user.Id));

                return new AuthenticateResultModel
                {
                    AccessToken = accessToken,
                    EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                    ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                    UserId = user.Id,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"{L("error")} {ex.Message}");
            }
        }

    }
}
