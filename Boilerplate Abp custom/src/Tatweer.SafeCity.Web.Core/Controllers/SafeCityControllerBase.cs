using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Tatweer.SafeCity.Controllers
{
    public abstract class SafeCityControllerBase: AbpController
    {
        protected SafeCityControllerBase()
        {
            LocalizationSourceName = SafeCityConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
