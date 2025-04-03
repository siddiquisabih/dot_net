using Abp.Authorization;
using Tatweer.SafeCity.Authorization.Roles;
using Tatweer.SafeCity.Authorization.Users;

namespace Tatweer.SafeCity.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
