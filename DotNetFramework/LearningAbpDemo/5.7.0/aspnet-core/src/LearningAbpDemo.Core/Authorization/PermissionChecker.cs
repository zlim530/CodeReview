using Abp.Authorization;
using LearningAbpDemo.Authorization.Roles;
using LearningAbpDemo.Authorization.Users;

namespace LearningAbpDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
