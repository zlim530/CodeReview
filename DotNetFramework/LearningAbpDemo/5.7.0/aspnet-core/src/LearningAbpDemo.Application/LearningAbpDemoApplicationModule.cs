using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LearningAbpDemo.Authorization;

namespace LearningAbpDemo
{
    [DependsOn(
        typeof(LearningAbpDemoCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class LearningAbpDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<LearningAbpDemoAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(LearningAbpDemoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
