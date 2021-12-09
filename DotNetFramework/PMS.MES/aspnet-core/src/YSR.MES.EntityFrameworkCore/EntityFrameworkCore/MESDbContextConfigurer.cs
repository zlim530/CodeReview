using System.Data.Common;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YSR.MES.EntityFrameworkCore
{
    public static class MESDbContextConfigurer
    {

        //public static void Configure(DbContextOptionsBuilder<MESDbContext> builder, string connectionString)
        //{
        //    builder.UseSqlServer(connectionString);
        //}

        //public static void Configure(DbContextOptionsBuilder<MESDbContext> builder, DbConnection connection)
        //{
        //    builder.UseSqlServer(connection);
        //}

        public static void Configure<T>(
            DbContextOptionsBuilder<T> dbContextOptions,
            string connectionString
            )
            where T : AbpDbContext
        {
            /* This is the single point to configure DbContextOptions for TaobaoAuthorizationDbContext */
            //dbContextOptions.UseSqlServer(connectionString);
            dbContextOptions.UseSqlServer(connectionString);
        }

        public static void Configure<T>(
            DbContextOptionsBuilder<T> dbContextOptions,
            DbConnection connection
            )
            where T : AbpDbContext
        {
            /* This is the single point to configure DbContextOptions for TaobaoAuthorizationDbContext */
            //dbContextOptions.UseSqlServer(connectionString);
            dbContextOptions.UseSqlServer(connection);
        }
    }
}
