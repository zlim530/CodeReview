using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YSR.MES.EntityFrameworkCore;
using YSR.MES.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace YSR.MES.Web.Tests
{
    [DependsOn(
        typeof(MESWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class MESWebTestModule : AbpModule
    {
        public MESWebTestModule(MESEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MESWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(MESWebMvcModule).Assembly);
        }
    }
}