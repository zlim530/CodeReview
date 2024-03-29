﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YSR.MES.Configuration;
using YSR.MES.Common;

namespace YSR.MES.Web.Host.Startup
{
    [DependsOn(
        typeof(MESWebCoreModule)
        , typeof(YSRCommonModule))]
    public class MESWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MESWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MESWebHostModule).GetAssembly());
        }
    }
}
