using DDDEFCoreOfRicherModel.Models;
using Microsoft.EntityFrameworkCore;

namespace DDDEFCoreOfRicherModel.DbContexts;

public class MyDbContext: DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Region> Regions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=demoone;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // 从当前程序集加载所有的 IEntityTypeConfiguration 
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}