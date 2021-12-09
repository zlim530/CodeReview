using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using YSR.MES.Common.DBPlatFormConsts;
using YSR.MES.Configuration;
using YSR.MES.EntityFrameworkCore.Routine;
using YSR.MES.Web;

namespace YSR.MES.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MESDbContextFactory : IDesignTimeDbContextFactory<MESDbContext>
    {
        public MESDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MESDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MESDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MESConsts.ConnectionStringName));

            return new MESDbContext(builder.Options);
        }
    }

    /// <summary>
    /// EF Core PMC commands 基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public abstract class MESDbContextFactory<T> : IDesignTimeDbContextFactory<T>
    //    where T : AbpDbContext
    //{
    //    /// <summary>
    //    /// 要采用的数据库连接节点名称
    //    /// </summary>
    //    public abstract string ConnectionStringName { get; }
    //    /// <summary>
    //    /// 创建DbContext实例
    //    /// 如果觉得每个数据库都要自己new太low，那么可以采用反射来动态创建，毕竟这里也只是PMC command使用的
    //    /// </summary>
    //    /// <param name="options"></param>
    //    /// <returns></returns>
    //    public abstract T CreateDbContext(DbContextOptions<T> options);
    //    public T CreateDbContext(string[] args)
    //    {
    //        var builder = new DbContextOptionsBuilder<T>();
    //        var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
    //        MESDbContextConfigurer.Configure(
    //            builder,
    //            configuration.GetConnectionString(ConnectionStringName)
    //        );
    //        return this.CreateDbContext(builder.Options);
    //    }
    //}
}
