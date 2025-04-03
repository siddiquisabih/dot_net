using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Tatweer.SafeCity.Authorization.Roles;
using Tatweer.SafeCity.Authorization.Users;
using Tatweer.SafeCity.MultiTenancy;
using Microsoft.Extensions.Logging;
using Abp.Domain.Uow;

namespace Tatweer.SafeCity.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IUnitOfWorkManager unitOfWorkManager,
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager signInManager,
            //ISystemClock systemClock,
            ILoggerFactory loggerFactory)
            : base(
                  options,
                  signInManager,
                  loggerFactory,
                  //systemClock,
                  unitOfWorkManager)
        {
        }
    }
}
