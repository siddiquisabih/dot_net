using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Microsoft.Extensions.Logging;
using Abp.Domain.Uow;
using Global.Project.Authorization.Users;
using Global.Project.MultiTenancy;
using Global.Project.Authorization.Roles;

namespace Global.Project.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IUnitOfWorkManager unitOfWorkManager,
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager signInManager,
            ILoggerFactory loggerFactory)
            : base(
                  options,
                  signInManager,
                  loggerFactory,
                  unitOfWorkManager)
        {
        }
    }
}
