using Microsoft.EntityFrameworkCore;
using UserMgr.Domain.Entities;

namespace UserMgr.Infrastracture.DbContexts
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; private set; }

        public DbSet<UserLoginHistory> UserLoginHistories { get; private set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 从当前程序集中加载聚合实体配置类
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
