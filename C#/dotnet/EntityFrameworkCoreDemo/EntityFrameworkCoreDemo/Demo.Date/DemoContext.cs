using Demo.Domian;
using Microsoft.EntityFrameworkCore;

/**
 * @author zlim
 * @create 2020/8/27 20:46:51
 */
namespace Demo.Date {
    public class DemoContext :DbContext{

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Data Source= .; Initial Catalog=demoone;Integrated Security=True");
        }

        public DbSet<League> Leagues { get; set; }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<Player> Players { get; set; }

    }
}
