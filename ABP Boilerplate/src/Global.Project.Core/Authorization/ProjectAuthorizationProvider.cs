using Abp.Authorization;
using Abp.Localization;
using Global.Project;

namespace Global.Project.Authorization
{
    public class ProjectAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));

 
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectConsts.LocalizationSourceName);
        }
    }
}
