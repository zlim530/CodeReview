using Microsoft.EntityFrameworkCore;

namespace LearningAbpDemo.Movie
{
    public class MovieDbContext : MovieDbContextBase
    {
        /*
        实体集通常对应数据库表;
        实体对应表中的行
        由于实体集包含多个实体，因此 DBSet 属性应为复数名称。 由于基架工具创建了 MovieLog DBSet，因此此步骤将其更改为复数 MovieLogs
        */
        public virtual DbSet<MovieLog> MovieLogs { get; set; }

        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
