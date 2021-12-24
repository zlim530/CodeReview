using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Localization;
using System.Collections.Generic;
using System.Linq;

namespace YSR.MES.Authorization
{
    public class SYSAuthorizationProvider : AuthorizationProvider
    {
        private readonly IRepository<Permission, long> _permissionRepository;
        private readonly List<Permission> _permissions;

        public SYSAuthorizationProvider(
            IRepository<Permission, long> permissionRepository
            )
        {
            _permissionRepository = permissionRepository;
            _permissions = _permissionRepository.GetAllList();
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
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
