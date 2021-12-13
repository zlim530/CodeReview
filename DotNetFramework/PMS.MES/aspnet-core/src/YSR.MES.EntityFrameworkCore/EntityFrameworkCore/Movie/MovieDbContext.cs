using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YSR.MES.Movie;

namespace YSR.MES.EntityFrameworkCore.Movie
{
    public class MovieDbContext : AbpDbContext
    {
        /// <summary>
        /// 电影信息表
        /// </summary>
        public DbSet<MovieInfo> MovieInfos { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }
    }
}
