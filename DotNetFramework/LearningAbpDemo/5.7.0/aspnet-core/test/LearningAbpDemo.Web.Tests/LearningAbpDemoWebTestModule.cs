using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LearningAbpDemo.EntityFrameworkCore;
using LearningAbpDemo.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace LearningAbpDemo.Web.Tests
{
    [DependsOn(
        typeof(LearningAbpDemoWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class LearningAbpDemoWebTestModule : AbpModule
    {
        public LearningAbpDemoWebTestModule(LearningAbpDemoEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LearningAbpDemoWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(LearningAbpDemoWebMvcModule).Assembly);
        }
    }
}