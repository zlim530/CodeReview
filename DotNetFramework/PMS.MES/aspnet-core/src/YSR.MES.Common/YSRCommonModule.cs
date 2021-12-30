using Abp.Modules;
using Abp.Reflection.Extensions;

namespace YSR.MES.Common
{
    public class YSRCommonModule : AbpModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YSRCommonModule).GetAssembly());
        }
    }
}
