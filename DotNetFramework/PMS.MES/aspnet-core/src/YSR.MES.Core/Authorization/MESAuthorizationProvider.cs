using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using System.Collections.Generic;
using System.Linq;

namespace YSR.MES.Authorization
{
    public class MESAuthorizationProvider : AuthorizationProvider
    {
        private readonly List<Permission> _permissions;

        public MESAuthorizationProvider()
        {

        }

        public MESAuthorizationProvider(List<Permission> permissions)
        {
            _permissions = permissions;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            /*context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);*/
            if (_permissions != null)
            {
                _permissions.Where(p => p.ParentId == 0).ToList().ForEach(p =>
                {
                    var permission = context.CreatePermission(p.Name, L(p.DisplayName));
                    CreateChildPermi(permission, p.Id);
                });
            }
        }

        private void CreateChildPermi(Abp.Authorization.Permission permission, long id)
        {
            var childPermission = _permissions.Where(s => s.ParentId == id);

            if (childPermission.Count() > 0)
            {
                childPermission.ToList().ForEach(z =>
                {
                    var grandPermi = permission.CreateChildPermission(z.Name, L(z.DisplayName));
                    CreateChildPermi(grandPermi, z.Id);
                });
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MESConsts.LocalizationSourceName);
        }
    }
}
