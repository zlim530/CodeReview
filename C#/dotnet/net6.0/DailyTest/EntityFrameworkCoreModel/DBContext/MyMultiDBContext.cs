using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreModel;

public class MyMultiDBContext : DbContext
{
    public DbSet<Book> Books { get; set; }


	public MyMultiDBContext(DbContextOptions<MyMultiDBContext> options) : base(options)
	{

	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}