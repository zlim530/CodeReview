using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YSR.MES.Routine;

namespace YSR.MES.EntityFrameworkCore.Routine
{
    public class RoutineDbContext : AbpDbContext
    {
        public DbSet<Companies> Companies { get; set; }

        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
