using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using YSR.MES.Authorization.Users;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace YSR.MES.Authorization.Roles
{
    public class RoleManager : AbpRoleManager<Role, User>
    {
        private readonly ICacheManager _cacheManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<RolePermissionSetting, long> _setrolepermissionRepository;

        public RoleManager(
            RoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<AbpRoleManager<Role, User>> logger,
            IPermissionManager permissionManager,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRoleManagementConfig roleManagementConfig,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository,
            IRepository<RolePermissionSetting, long> setrolepermissionRepository
            )
            : base(
                  store,
                  roleValidators,
                  keyNormalizer,
                  errors, logger,
                  permissionManager,
                  cacheManager,
                  unitOfWorkManager,
                  roleManagementConfig,
                organizationUnitRepository,
                organizationUnitRoleRepository)
        {
            _cacheManager = cacheManager;
            _permissionManager = permissionManager;
            _unitOfWorkManager = unitOfWorkManager;
            _setrolepermissionRepository = setrolepermissionRepository;
        }

        /// <summary>
        /// 重写获取角色权限方法
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public override async Task<IReadOnlyList<Abp.Authorization.Permission>> GetGrantedPermissionsAsync(Role role)
        {
            var cacheItem = GetRolePermissionCacheItem(role.Id);
            var allPermissions = _permissionManager.GetAllPermissions();
            var result = allPermissions.Where(p => cacheItem.GrantedPermissions.Contains(p.Name)).ToList();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取用户角色权限缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RolePermissionCacheItem GetRolePermissionCacheItem(int roleId)
        {
            var cacheKey = roleId + "@" + (GetCurrentTenantId() ?? 0);
            return _cacheManager.GetRolePermissionCache().Get(cacheKey, _ =>
            {
                var newCacheItem = new RolePermissionCacheItem(roleId);
                var permissions = _setrolepermissionRepository.GetAll()
                                        .Where(r => r.RoleId == roleId && r.IsGranted)
                                        .Select(r => r.Name)
                                        .ToList();
                var list = _permissionManager.GetAllPermissions();
                foreach (var permission in list.Where(l => permissions.Contains(l.Name)))
                {
                    newCacheItem.GrantedPermissions.Add(permission.Name);
                }
                return newCacheItem;
            });

        }

        /// <summary>
        /// 获取多租户ID
        /// </summary>
        /// <returns></returns>
        private int? GetCurrentTenantId()
        {
            if (_unitOfWorkManager.Current != null)
            {
                return _unitOfWorkManager.Current.GetTenantId();
            }

            return AbpSession.TenantId;
        }
    }
}
