using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YSR.MES.Authorization;

namespace YSR.MES
{
    [DependsOn(
        typeof(MESCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MESApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MESAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MESApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
