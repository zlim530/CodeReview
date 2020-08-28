using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LearningAbpDemo.EntityFrameworkCore
{
    public static class LearningAbpDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LearningAbpDemoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LearningAbpDemoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
