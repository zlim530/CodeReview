using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LearningAbpDemo.Configuration;

namespace LearningAbpDemo.Web.Host.Startup
{
    [DependsOn(
       typeof(LearningAbpDemoWebCoreModule))]
    public class LearningAbpDemoWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public LearningAbpDemoWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LearningAbpDemoWebHostModule).GetAssembly());
        }
    }
}
