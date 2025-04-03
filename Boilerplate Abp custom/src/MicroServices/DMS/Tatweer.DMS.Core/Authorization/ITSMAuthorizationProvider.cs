using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Tatweer.ITSM.Authorization
{
    public class ITSMAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
           
            //context.CreatePermission(PermissionNames., L("Users"));
            //context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            //context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            //var blogService = context.GetPermissionOrNull(SmartOfficerPermissions.SmartOfficerService)
            //                       ?? context.CreatePermission(SmartOfficerPermissions.SmartOfficerService, L("SmartOfficerService"), multiTenancySides: MultiTenancySides.Tenant);

            //var blogs = blogService.CreateChildPermission(SmartOfficerPermissions.SmartOfficer.Default, L("SmartOfficerPermissions.SmartOfficer"), multiTenancySides: MultiTenancySides.Tenant);

            //blogs.CreateChildPermission(SmartOfficerPermissions.SmartOfficer.Create, L("SmartOfficerPermissions.SmartOfficer.Permissions.Create"), multiTenancySides: MultiTenancySides.Tenant);
            //blogs.CreateChildPermission(SmartOfficerPermissions.SmartOfficer.Edit, L("SmartOfficerPermissions.SmartOfficer.Permissions.Edit"), multiTenancySides: MultiTenancySides.Tenant);
            //blogs.CreateChildPermission(SmartOfficerPermissions.SmartOfficer.Delete, L("SmartOfficerPermissions.SmartOfficer.Permissions.Delete"), multiTenancySides: MultiTenancySides.Tenant);

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ITSMConsts.LocalizationSourceName);
        }
    }
}
