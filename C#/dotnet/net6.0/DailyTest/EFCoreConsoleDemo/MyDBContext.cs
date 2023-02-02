using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreConsoleDemo
{
	public class MyDBContext : DbContext
	{
        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(l => l.AddConsole());

        public DbSet<Book> Books { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Leave> Leaves { get; set; }

        public DbSet<OrgUnit> OrgUnits { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = "";
            optionsBuilder.
                            UseSqlServer(connStr)
                            //UseNpgsql("Server=localhost;Port=5432;Database=MyProjectName;User Id=postgres;Password=root;")
                            .UseLoggerFactory(loggerFactory)
                            //.LogTo(log =>
                            //{
                            //    if (!log.Contains("CommandExecuting")) return;
                            //    Console.WriteLine(log);
                            //})
                            ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 从当前程序集加载所有的 IEntityTypeConfiguration 
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}