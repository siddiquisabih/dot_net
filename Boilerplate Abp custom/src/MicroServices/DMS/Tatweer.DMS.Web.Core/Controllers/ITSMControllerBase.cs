using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Tatweer.ITSM.Controllers
{
    public abstract class ITSMControllerBase: AbpController
    {
        protected ITSMControllerBase()
        {
            LocalizationSourceName = ITSMConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
