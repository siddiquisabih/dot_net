using Abp.Authorization;
using Tatweer.RadarManagment.Authorization.Roles;
using Tatweer.RadarManagment.Authorization.Users;

namespace Tatweer.RadarManagment.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
