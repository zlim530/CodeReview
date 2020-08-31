using Demo.Domian;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

/**
 * @author zlim
 * @create 2020/8/27 20:46:51
 */
namespace Demo.Date {
    public class DemoContext :DbContext{

        //public DemoContext() {
        //    ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source= .; Initial Catalog=demoone;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // 在 OnModelCreating  方法中，匿名对象中的属性就是多对多关系中的联合主键
            // HasKey 方法中的 x 参数就是 GamePlayer
            modelBuilder.Entity<GamePlayer>().HasKey(x => new { x.PlayerId,x.GameId});

            modelBuilder.Entity<Resume>()// EntityTypeBuilder<Resume>
                .HasOne(x => x.Player)// navigationExpression:x:Resume => x.Player; 返回值为：ReferenceNavigationBuilder<Resume,Player> (ReferenceNavigationBuilder<TEntity, TRelatedEntity>)
                .WithOne(x => x.Resume)// navigationExpression:x:Player => x.Resume
                // 前面 Entity 泛型中放 Player 和 Resume 都可以，但是在 HasForeignKey 方法中泛型参数只能放 Resume
                .HasForeignKey<Resume>(x => x.PlayerId);
        }

        public DbSet<League> Leagues { get; set; }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<Player> Players { get; set; }

        // 将执行的 SQL 语句打印到控制台上
        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                   .AddConsole();
        });


    }
}
