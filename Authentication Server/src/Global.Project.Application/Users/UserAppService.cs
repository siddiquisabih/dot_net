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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System;
using Abp.ObjectMapping;

using Abp.Domain.Uow;
using System.Transactions;
using System.IO;
using Castle.MicroKernel.Registration;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;
using PostmarkDotNet;
using System.Net;
using static Abp.Net.Mail.EmailSettingNames;
using System.Net.Mail;
using Global.Project.Authorization.Accounts;
using Global.Project.Roles.Dto;
using Global.Project.Users.Dto;
using Global.Project.Auditor;
using Global.Project.Departments;
using Global.Project.Authorization.Users;
using Global.Project.Authorization;
using Global.Project.Common;
using Global.Project.Authorization.Roles;
using Global.Project.Model;
using Global.Project.Enums;

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
        private readonly IMemoryCache _memoryCache;
        private readonly IDataService _dataService;
        private readonly IAuditorAppService _auditLogBusiness;

        private readonly IObjectMapper _objectMapper;
        private readonly DepartmentAppService _departmentAppService;

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<ProjectAttachment> _attachmentReposioty;


        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IMemoryCache memoryCache,
            IDataService dataService,
            IAuditorAppService auditorAppService,
            DepartmentAppService departmentAppService,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<ProjectAttachment> attachmentReposioty
            )
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _memoryCache = memoryCache;
            _dataService = dataService;
            _auditLogBusiness = auditorAppService;
            _departmentAppService = departmentAppService;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _attachmentReposioty = attachmentReposioty;
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            try
            {
                CheckCreatePermission();

                var user = ObjectMapper.Map<User>(input);

                user.IsActive = true;
                user.TenantId = AbpSession.TenantId;
                user.IsEmailConfirmed = true;

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

                MapToEntity(input, user);

                user.IsActive = true;

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
                var PasswordResetCode =Convert.ToString(rnd.Next(10000, 50000));
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

              return  HttpStatusCode.OK;

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
                if(user == null)
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

            if(user.PasswordResetCode==input.OTPCode)
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
            catch (Exception )
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
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

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
                _dataService.RefreshMapping();
                var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == input.Id).Result;

                if (user == null)
                {
                    throw new EntityNotFoundException(typeof(UserDto), input.Id);
                }
                var userDto = MapToEntityDto(user);


                var userProfileAttachment = await _attachmentReposioty.GetAll().Where(x => x.EntityId == userDto.Id && x.AttachmentType == ProjectAttachmentsEnum.UserProfile.ToString()).OrderByDescending(x => x.Id).Select(x => new ProjectAttachmentDto
                {
                    Id = x.Id,
                    EntityId = x.EntityId,
                    AttachmentName = x.AttachmentName,
                    AttachmentPath = x.AttachmentPath,
                    AttachmentType = x.AttachmentType,
                    FileType = x.FileType
                }).FirstOrDefaultAsync();


                if (userProfileAttachment?.AttachmentPath != null)
                {
                    userProfileAttachment.AttachmentPath = userProfileAttachment.AttachmentPath;
                    if (File.Exists(userProfileAttachment.AttachmentPath))
                    {
                        userDto.UserProfilePictureUrl = File.ReadAllBytes(userProfileAttachment.AttachmentPath);
                    }
                }


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
                var mappingList = _dataService.Get();

                if (mappingList != null && mappingList.Any())
                {
                    var myDepartment = mappingList.FirstOrDefault(m => m.UserId == input.Id);
                    if (myDepartment != null)
                    {
                        userDto.DepartmentId = myDepartment.DepartmentId;
                        userDto.SupervisorId = myDepartment.SupervisorId;
                    }
                }
                else
                {
                    _dataService.RefreshMapping();
                    mappingList = _dataService.Get();
                    if (mappingList != null && mappingList.Any())
                    {
                        var myDepartment = mappingList.FirstOrDefault(m => m.UserId == AbpSession.UserId);
                        if (myDepartment != null)
                        {
                            userDto.DepartmentId = myDepartment.DepartmentId;
                            userDto.SupervisorId = myDepartment.SupervisorId;
                        }
                    }
                }


                return userDto;

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public override async Task<PagedResultDto<UserDto>> GetAllAsync(PagedUserResultRequestDto input)
        {

            var data = await base.GetAllAsync(input);

            var mappingList = _dataService.Get();

            if (mappingList == null)
            {
                _dataService.RefreshMapping();
                mappingList = _dataService.Get();
            }

            if(_abpSession != null)
            {
                long currentUserId = _abpSession.UserId.GetValueOrDefault();
                if (currentUserId > 0)
                {
                    var currentUser = await _userManager.GetUserByIdAsync(currentUserId);

                    if (await _userManager.IsInRoleAsync(currentUser, RoleEnum.Customer.ToString()))
                    {
                        var items = data.Items.Where(i => i.Id == AbpSession.UserId).ToList();
                        data.Items = items;
                        data.TotalCount = items.Count;
                    }
                }

            }




            if (PermissionChecker.IsGranted(PermissionNames.ViewAllUsers) || PermissionChecker.IsGranted(PermissionNames.ViewServiceDeskIncidentRequests))
            {
            }
            else if (PermissionChecker.IsGranted(PermissionNames.ViewDepartmentManagedUsers) && mappingList != null)
            {
                var myDepartmentId = mappingList.FirstOrDefault(m => m.UserId == AbpSession.UserId)?.DepartmentId;
                if (myDepartmentId.HasValue)
                {
                    var departmentUsers = mappingList.Where(m => m.DepartmentId == myDepartmentId).ToList().Select(m => m.UserId).ToList();
                    var items = data.Items.Where(i => departmentUsers.Contains((int)i.Id) && i.Id != AbpSession.UserId).ToList();
                    data.Items = items;
                    data.TotalCount = items.Count;
                }
            }
            else if (PermissionChecker.IsGranted(PermissionNames.ViewDepartmentSupverisedUsers) && mappingList != null)
            {
                var mySupervisedUsers = mappingList.Where(m => m.SupervisorId == AbpSession.UserId).ToList().Select(m => m.UserId).ToList();
                if (mySupervisedUsers != null && mySupervisedUsers.Count > 0)
                {
                    var items = data.Items.Where(i => mySupervisedUsers.Contains((int)i.Id)).ToList();
                    data.Items = items;
                    data.TotalCount = items.Count;
                }
            }
            else
            {
                data.Items = data.Items.Where(m => m.Id == AbpSession.UserId).ToList();
                data.TotalCount = 1;
            }
            return data;
        }
    }
}

