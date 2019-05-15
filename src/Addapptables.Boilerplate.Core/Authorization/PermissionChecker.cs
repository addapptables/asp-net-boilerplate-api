using Abp.Authorization;
using Addapptables.Boilerplate.Authorization.Roles;
using Addapptables.Boilerplate.Authorization.Users;

namespace Addapptables.Boilerplate.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
