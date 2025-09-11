using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Net;
using Global.Project.Authorization.Users;
using Global.Project.Authorization;
using Global.Project.Authorization.Roles;
using Global.Project.Enums;
using Global.Project.Users.Dto;
using Global.Project.Auditor;
using Global.Project.Authorization.Accounts;
using Global.Project.Roles.Dto;

namespace Global.Project.Users
{
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IAuditorAppService _auditLogBusiness;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IAuditorAppService auditorAppService)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _auditLogBusiness = auditorAppService;
            LocalizationSourceName = ProjectConsts.LocalizationSourceName;

        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            try
            {
                CheckCreatePermission();

                var user = ObjectMapper.Map<User>(input);
                user.EmailAddress = input.EmailAddress.ToLower();
                user.IsActive = true;
                user.TenantId = AbpSession.TenantId;
                user.IsEmailConfirmed = true;

                var isExist = await _userManager.FindByEmailAsync(input.EmailAddress);

                if (isExist != null)
                {
                    throw new UserFriendlyException(L("email_exist_try_another", input.EmailAddress));
                }

                await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

                CheckErrors(await _userManager.CreateAsync(user, input.Password));

                if (input.RoleNames != null)
                {
                    CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
                }

                CurrentUnitOfWork.SaveChanges();

                _auditLogBusiness.InsertAuditLog("CreateUser", Convert.ToInt32(AbpSession.UserId), Convert.ToInt32(user.Id), "User");

                return MapToEntityDto(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            try
            {
                CheckUpdatePermission();

                var user = await _userManager.GetUserByIdAsync(input.Id);
                input.EmailAddress = user.EmailAddress;
                MapToEntity(input, user);

                //user.IsActive = true;
                CheckErrors(await _userManager.UpdateAsync(user));

                if (input.RoleNames != null)
                {
                    CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
                }

                _auditLogBusiness.InsertAuditLog("UpdateUser", Convert.ToInt32(AbpSession.UserId), Convert.ToInt32(user.Id), "User");

                return await GetAsync(input);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task<ListResultDto<UserDto>> GetUserByRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return new ListResultDto<UserDto>(ObjectMapper.Map<List<UserDto>>(users));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();


            //if (user.UserProfileImage != null)
            //{
            //    userDto.UserProfileImage = ObjectMapper.Map<UserProfileImageDto>(user.UserProfileImage);
            //}

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            var users = Repository.GetAllIncluding(x => x.Roles);
            return users;
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<HttpStatusCode> ResetPasswordRequest(ResetPasswordRequestDto input)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(input.EmailAddress);

                if (user == null)
                {
                    throw new UserFriendlyException("Account with this email is not exist, please try with valid email.");
                }

                Random rnd = new Random();
                var PasswordResetCode = Convert.ToString(rnd.Next(10000, 50000));
                user.PasswordResetCode = PasswordResetCode;
                StringBuilder body = new StringBuilder();
                body.Append("Hi ");
                body.Append(user.FullName);
                body.Append(", We received password reset request,Here is password reset code");
                body.Append("<br/>");
                body.Append(user.PasswordResetCode);

                try
                {
                }
                catch (Exception e)
                {
                    throw;
                }

                await _userManager.UpdateAsync(user);

                return HttpStatusCode.OK;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<HttpStatusCode> ExpireOldOTP(ExpireOldOTPDto input)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(input.EmailAddress);
                if (user == null)
                {
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    user.PasswordResetCode = null;
                    await _userManager.UpdateAsync(user);
                    CurrentUnitOfWork.SaveChanges();
                    return HttpStatusCode.OK;
                }

            }
            catch (Exception)
            {
                throw new UserFriendlyException("An error occured please try again lator");
            }

        }

        public async Task<HttpStatusCode> CheckOTP(CheckOTPDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.EmailAddress);

            if (user.PasswordResetCode == input.OTPCode)
            {
                return HttpStatusCode.OK;
            }
            else
            {
                throw new UserFriendlyException("OTP has been expired or it's is not correct,Please check and enter correct OTP");
            }
        }

        public async Task<HttpStatusCode> ChangePasswordByOTP(PasswordChangeRequestDto input)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(input.EmailAddress);

                if (user != null)
                {

                    if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.Password))
                    {
                        throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
                    }
                    user.Password = _passwordHasher.HashPassword(user, input.Password);
                    user.PasswordResetCode = null;
                    await _userManager.UpdateAsync(user);
                    CurrentUnitOfWork.SaveChanges();
                    return HttpStatusCode.OK;
                }

                throw new UserFriendlyException("User is not exist");
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }

        public async Task<UserDto> GetUserById(long id)
        {
            var user = await Repository.GetAll()
                .Include(x => x.Roles)
                .Include(x => x.UserProfileImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(UserDto), id);
            }

            var userDto = MapToEntityDto(user);
            var roleIds = user.Roles.Select(x => x.RoleId).ToList();
            var permissions = new List<string>();
            var roleName = new List<string>();

            foreach (var roleId in roleIds)
            {
                var role = await _roleManager.GetRoleByIdAsync(roleId);
                roleName.Add(role.Name);
                var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                permissions.AddRange(grantedPermissions.Select(x => x.Name).ToList());
            }

            userDto.Role = roleName.ToArray();
            userDto.Permissions = permissions.Distinct().ToArray();


            return userDto;
        }

        public override async Task<UserDto> GetAsync(EntityDto<long> input)
        {
            try
            {

                var user = Repository.GetAll()
                    .Include(x => x.Roles)
                    .Include(x => x.UserProfileImage)
                    .FirstOrDefaultAsync(x => x.Id == input.Id).Result;

                if (user == null)
                {
                    throw new EntityNotFoundException(typeof(UserDto), input.Id);
                }
                var userDto = MapToEntityDto(user);

                var roleIds = user.Roles.Select(x => x.RoleId).ToList();
                var permissions = new List<string>();
                var roleName = new List<string>();

                foreach (var roleId in roleIds)
                {
                    var role = _roleManager.GetRoleByIdAsync(roleId).Result;
                    roleName.Add(role.Name);
                    var grantedPermissions = _roleManager.GetGrantedPermissionsAsync(role).Result.ToArray();
                    permissions.AddRange(grantedPermissions.Select(x => x.Name).ToList());
                }

                userDto.Role = roleName.ToArray();
                userDto.Permissions = permissions.Distinct().ToArray();
                return userDto;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override async Task<PagedResultDto<UserDto>> GetAllAsync(PagedUserResultRequestDto input)
        {

            try
            {
                List<UserDto> userDtos = [];
                var users = await Repository.GetAll()
                    .Include(x => x.Roles)
                    .Include(x => x.UserProfileImage)
                    .ToListAsync();

                var totalCount = users.Count();

                foreach (var user in users)
                {

                    var userData = MapToEntityDto(user);

                    var roleIds = user.Roles.Select(x => x.RoleId).ToList();
                    var permissions = new List<string>();
                    var roleName = new List<string>();

                    foreach (var roleId in roleIds)
                    {
                        var role = await _roleManager.GetRoleByIdAsync(roleId);
                        roleName.Add(role.Name);
                        var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                        permissions.AddRange(grantedPermissions.Select(x => x.Name).ToList());
                    }

                    userData.Role = roleName.ToArray();
                    userData.Permissions = permissions.Distinct().ToArray();

                    userDtos.Add(userData);
                }

                if (_abpSession?.UserId != null)
                {
                    var currentUser = await _userManager.GetUserByIdAsync(_abpSession.UserId.Value);

                    if (await _userManager.IsInRoleAsync(currentUser, RoleEnum.Customer.ToString()))
                    {
                        userDtos = userDtos.Where(i => i.Id == _abpSession.UserId.Value).ToList();
                        totalCount = userDtos.Count;
                    }

                }
                return new PagedResultDto<UserDto>(totalCount, userDtos);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(L("error", ex.Message));
            }

        }
    }
}

