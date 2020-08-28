using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using LearningAbpDemo.Configuration;
using LearningAbpDemo.Web;

namespace LearningAbpDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class LearningAbpDemoDbContextFactory : IDesignTimeDbContextFactory<LearningAbpDemoDbContext>
    {
        public LearningAbpDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LearningAbpDemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            LearningAbpDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(LearningAbpDemoConsts.ConnectionStringName));

            return new LearningAbpDemoDbContext(builder.Options);
        }
    }
}
