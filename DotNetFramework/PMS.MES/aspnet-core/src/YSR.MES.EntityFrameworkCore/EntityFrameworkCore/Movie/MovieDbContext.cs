using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        

    }
}
