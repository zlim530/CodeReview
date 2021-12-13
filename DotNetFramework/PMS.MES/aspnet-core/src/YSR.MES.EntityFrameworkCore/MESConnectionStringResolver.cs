using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Microsoft.Extensions.Configuration;
using System;
using YSR.MES.Common.DBPlatFormConsts;
using YSR.MES.Configuration;
using YSR.MES.EntityFrameworkCore;
using YSR.MES.EntityFrameworkCore.Movie;
using YSR.MES.EntityFrameworkCore.Routine;
using YSR.MES.Web;

namespace YSR.MES
{
    public class MESConnectionStringResolver : DefaultConnectionStringResolver
    {
        //private readonly IConfigurationRoot _appConfiguration;

        public MESConnectionStringResolver(IAbpStartupConfiguration configuration)
            : base(configuration)
        {
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var connectStringName = GetConnectionStringName(args);
            if (connectStringName != null)
            {
                var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
                return configuration.GetConnectionString(connectStringName);
            }
            return base.GetNameOrConnectionString(args);
        }

        private string GetConnectionStringName(ConnectionStringResolveArgs args)
        {
            var type = args["DbContextConcreteType"] as Type;
            if (type == typeof(MESDbContext))
            {
                return MESDBContextPlatFormConst.DefaultConnectionStringName;
            }
            else if (type == typeof(RoutineDbContext))
            {
                return MESDBContextPlatFormConst.RouTineTestConnectionStringName;
            }
            else if (type == typeof(MovieDbContext))
            {
                return MESDBContextPlatFormConst.MovieDBConnectionStringName;
            }

            //采用默认数据库
            return MESDBContextPlatFormConst.DefaultConnectionStringName;
        }
    }
}
