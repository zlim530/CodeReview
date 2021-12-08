using Abp.Authorization;
using YSR.MES.Authorization.Roles;
using YSR.MES.Authorization.Users;

namespace YSR.MES.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
