using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using YSR.MES.Authorization.Roles;
using YSR.MES.Authorization.Users;
using YSR.MES.MultiTenancy;
using Abp.Authorization.Roles;

namespace YSR.MES.EntityFrameworkCore
{
    public class MESDbContext : AbpZeroDbContext<Tenant, Role, User, MESDbContext>
    {
        /* Define a DbSet for each entity of the application */

        /// <summary>
        /// 系统权限表
        /// </summary>
        public DbSet<Permission> SysPermissions { get; set; }

        /// <summary>
        /// 角色权限关系表
        /// </summary>
        public DbSet<RolePermissionSetting> RolePermissionSettings { get; set; }


        public MESDbContext(DbContextOptions<MESDbContext> options)
            : base(options)
        {
        }
    }
}
