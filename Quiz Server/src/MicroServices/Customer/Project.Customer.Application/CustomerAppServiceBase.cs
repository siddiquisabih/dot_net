using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;

namespace Project.Customer
{
    public abstract class CustomerAppServiceBase : ApplicationService
    {

        protected CustomerAppServiceBase()
        {
            LocalizationSourceName = CustomerConsts.LocalizationSourceName;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
