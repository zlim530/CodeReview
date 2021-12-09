using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using YSR.MES.EntityFrameworkCore.Routine;
using YSR.MES.EntityFrameworkCore.Seed;

namespace YSR.MES.EntityFrameworkCore
{
    [DependsOn(
        typeof(MESCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class MESEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(Component.For<IConnectionStringResolver, IDbPerTenantConnectionStringResolver>()
                                                        .ImplementedBy<MESConnectionStringResolver>()
                                                        .LifestyleTransient());

            if (!SkipDbContextRegistration)
            {
                AddDbContext<MESDbContext>();
                AddDbContext<RoutineDbContext>();
                //AddDbContext<SunLightDbContext>();
                //Configuration.Modules.AbpEfCore().AddDbContext<MESDbContext>(options =>
                //{
                //    if (options.ExistingConnection != null)
                //    {
                //        MESDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                //    }
                //    else
                //    {
                //        MESDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                //    }
                //});
            }
        }

        /// <summary>
        /// 注入多个数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void AddDbContext<T>() where T : AbpDbContext
        {
            Configuration.Modules.AbpEfCore().AddDbContext<T>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    MESDbContextConfigurer.Configure<T>(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    MESDbContextConfigurer.Configure<T>(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MESEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
