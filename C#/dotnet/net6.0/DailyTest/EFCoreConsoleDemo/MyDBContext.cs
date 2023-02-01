using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreConsoleDemo
{
	public class MyDBContext : DbContext
	{
        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(l => l.AddConsole());

        public DbSet<Book> Books { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = "Server=127.0.0.1;Database=MiniKitchen;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
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
            // �ӵ�ǰ���򼯼������е� IEntityTypeConfiguration 
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}