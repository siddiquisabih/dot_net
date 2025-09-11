using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Abp.UI;

using Global.Project.Auditor;
using Global.Project.Roles.Dto;
using Global.Project.Authorization;
using Global.Project.Authorization.Roles;
using Global.Project.Authorization.Users;

namespace Global.Project.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IAuditorAppService _auditLogBusiness;

        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager, IAuditorAppService auditLogBusiness)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _auditLogBusiness = auditLogBusiness;
        }


        public override async Task<PagedResultDto<RoleDto>> GetAllAsync(PagedRoleResultRequestDto input)
        {
            var data = await CreateFilteredQuery(input).Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DisplayName = x.DisplayName,
                GrantedPermissions = x.Permissions.Where(y=>y.IsGranted==true).Select(x => x.Name).ToList(),
                NormalizedName = x.NormalizedName

            }).ToListAsync();
            return new PagedResultDto<RoleDto>(data.Count(), data);
        }
        public override async Task<RoleDto> CreateAsync(CreateRoleDto input)
        {
            try
            {
                CheckCreatePermission();


                var RolesList = await _roleManager.Roles.ToListAsync();
                bool RoleAlreadyExit = RolesList.Any(x => x.Name == input.Name);
                if (RoleAlreadyExit)
                {
                    throw new UserFriendlyException("Role with this name is already exist");
                };
                var role = ObjectMapper.Map<Role>(input);
               var PreviousRolePermanantId = RolesList.Select(x => x.Id).LastOrDefault();
                //role.Id = PreviousRolePermanantId + 1;
                role.SetNormalizedName();

                CheckErrors(await _roleManager.CreateAsync(role));

                var grantedPermissions = PermissionManager
                    .GetAllPermissions()
                    .Where(p => input.GrantedPermissions.Contains(p.Name))
                    .ToList();

                await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

                _auditLogBusiness.InsertAuditLog("CreateRole", Convert.ToInt32(AbpSession.UserId), role.Id, "Role");

                return MapToEntityDto(role);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public override async Task<RoleDto> UpdateAsync(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            _auditLogBusiness.InsertAuditLog("UpdateRole", Convert.ToInt32(AbpSession.UserId), role.Id, "Role");

            return MapToEntityDto(role);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList()
            ));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedRoleResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedRoleResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}

