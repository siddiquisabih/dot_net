using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using Project.Customer;

namespace Project.Customer.Controllers
{
    public abstract class CustomerControllerBase: AbpController
    {
        protected CustomerControllerBase()
        {
            LocalizationSourceName = CustomerConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
