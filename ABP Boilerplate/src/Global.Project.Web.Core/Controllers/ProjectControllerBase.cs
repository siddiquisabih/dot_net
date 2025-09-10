using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Global.Project;
using Microsoft.AspNetCore.Identity;

namespace Global.Project.Controllers
{
    public abstract class ProjectControllerBase: AbpController
    {
        protected ProjectControllerBase()
        {
            LocalizationSourceName = ProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
