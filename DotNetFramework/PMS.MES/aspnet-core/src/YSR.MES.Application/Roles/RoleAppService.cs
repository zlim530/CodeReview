using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using YSR.MES.Authorization;
using YSR.MES.Authorization.Roles;
using YSR.MES.Authorization.Users;
using YSR.MES.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp;
using YSR.MES.Routine;
using System;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Authorization.Roles;

namespace YSR.MES.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Permission, long> _sysPermissionRepository;
        private readonly IRepository<Companies, Guid> _companiesRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly ICacheManager _cacheManager;

        public RoleAppService(IRepository<Role> repository
            , RoleManager roleManager
            , UserManager userManager
            , IRepository<Permission, long> sysPermissionRepository
            , IRepository<Companies, Guid> companiesRepository
            , IRepository<OrganizationUnit, long> organizationUnitRepository
            , PermissionManager permissionManager
            , ICacheManager cacheManager)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _sysPermissionRepository = sysPermissionRepository;
            _companiesRepository = companiesRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _permissionManager = permissionManager;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取所有组织机构
        /// </summary>
        /// <returns></returns>
        public List<OrganizationUnit> GetAllOrganizationList()
        {
            var orgList = _cacheManager.GetCache("Organization").Get("AllOrganization", _ => _organizationUnitRepository.GetAllList());
            return (List<OrganizationUnit>)orgList;
        }

        /// <summary>
        /// 创建组织机构
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateOrganizationUnitAsync()
        {
            var entity = new OrganizationUnit();
            entity.DisplayName = "YSR";
            entity.Code = "001";

            await _organizationUnitRepository.InsertAsync(entity);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 写 Redis 缓存并获取
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetRedisCacheStringAsync()
        {
            _cacheManager.GetCache("Test").Set("1", "YSR");

            var sessionKey = _cacheManager.GetCache("Test").Get("1", (val) => val) as string;

            return await Task.FromResult(sessionKey);
        }

        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        //public async Task<RolePermissionCacheItem> GetRolePermissionCacheItemAsync()
        //{
        //    var cacheKey = 1 + "@" + 0;
        //    var list = _permissionManager.GetAllPermissions();
        //    return await _cacheManager.GetRolePermissionCache().GetAsync(cacheKey, () =>
        //    {
        //        var newCacheItem = new RolePermissionCacheItem();
        //        newCacheItem.GrantedPermissions = list.Select(p => p.Name).ToHashSet();
        //        return newCacheItem;
        //    });
        //}

        /// <summary>
        /// 获取所有公司
        /// </summary>
        /// <returns></returns>
        public async Task<List<Companies>> GetAllCompaniesAsync()
        {
            var temp = await _companiesRepository.GetAllListAsync();
            return temp;
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<Permission>> GetAllPermissionAsync()
        {
            var list = _permissionManager.GetAllPermissions();
            var data = await _sysPermissionRepository.GetAll().ToListAsync();
            if (list.Count() != data.Count())
            {
                throw new AbpException("Error!!!");
            }
            return data;
        }

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreatePermissionAsync()
        {
            var entity = new Permission();
            entity.ParentId = 0;
            entity.DisplayName = "PMS";
            entity.Name = "PMS";
            entity.Description = "PMS系统管理权限";
            entity.Module = "PMS";

            await _sysPermissionRepository.InsertAsync(entity);

            return await Task.FromResult(true);
        }

        public override async Task<RoleDto> CreateAsync(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        public override async Task<RoleDto> UpdateAsync(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList()
            ));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedRoleResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedRoleResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}

