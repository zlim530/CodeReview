using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace YSR.MES.EntityFrameworkCore
{
    public static class MESDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MESDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MESDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
