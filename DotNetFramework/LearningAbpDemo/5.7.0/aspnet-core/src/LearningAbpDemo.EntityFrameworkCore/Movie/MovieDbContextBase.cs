using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LearningAbpDemo.Movie
{
    public class MovieDbContextBase : AbpDbContext
    {
        public MovieDbContextBase(DbContextOptions options) : base(options)
        {
        }


        #region 映射模型
        /// <summary>
        /// 映射模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //获取所有程序集
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            assemblies.ToList().ForEach(s =>
            {
                //过滤掉 IFullAudited 接口自动生成的模型
                var types = s.GetTypes().Where(e => !e.IsAbstract).Where(e => e.GetCustomAttributes().Contains(new NotMappedAttribute())).ToList();
                foreach (var type in types)
                {
                    modelBuilder.Ignore(type);
                }
            });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieLog>(m => {
                m.ToTable("MovieLog");
                m.Property(x => x.Title).IsRequired(true).HasMaxLength(50);
                m.Property(x => x.RelaseDate).IsRequired(true).HasMaxLength(50);
                m.Property(x => x.Genre).IsRequired(true).HasMaxLength(50);
                m.Property(x => x.Price).IsRequired(true);
            });
        }

        #endregion


    }
}
