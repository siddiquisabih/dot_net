using Abp.Authorization;
using Global.Project.Authorization.Roles;
using Global.Project.Authorization.Users;

namespace Global.Project.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
