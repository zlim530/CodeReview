using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace YSR.MES.Movie
{
    [DependsOn(
        typeof(MESCoreModule),
        typeof(AbpAutoMapperModule)
    )]
    public class MovieModule : AbpModule
    {
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            // 注册当前程序集到 IoC 容器
            IocManager.RegisterAssemblyByConvention(typeof(MovieModule).GetAssembly());

            //加载映射
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                cfg => cfg.AddMaps(typeof(MovieModule).GetAssembly())
            );
        }
        #endregion
    }
}
