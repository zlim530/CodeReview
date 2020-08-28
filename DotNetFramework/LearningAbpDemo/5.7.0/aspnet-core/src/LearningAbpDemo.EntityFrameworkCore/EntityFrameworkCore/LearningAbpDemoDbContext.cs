using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LearningAbpDemo.Authorization.Roles;
using LearningAbpDemo.Authorization.Users;
using LearningAbpDemo.MultiTenancy;
using LearningAbpDemo.Test;

namespace LearningAbpDemo.EntityFrameworkCore
{
    public class LearningAbpDemoDbContext : AbpZeroDbContext<Tenant, Role, User, LearningAbpDemoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DbSet<Test.Movie> Movies { get; set; }

        public LearningAbpDemoDbContext(DbContextOptions<LearningAbpDemoDbContext> options)
            : base(options)
        {
        }

        
        /// <summary>
        /// 映射模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test.Movie>( p => {
                p.ToTable("Movies","test");
                p.Property(x => x.Name).IsRequired(true).HasMaxLength(20);
                p.Property(x => x.Starts).HasMaxLength(100);
            });
        }

    }
}
