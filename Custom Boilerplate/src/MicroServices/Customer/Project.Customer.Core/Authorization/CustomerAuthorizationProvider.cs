using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using Project.Customer;

namespace Project.Customer.Authorization
{
    public class CustomerAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
           
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CustomerConsts.LocalizationSourceName);
        }
    }
}
