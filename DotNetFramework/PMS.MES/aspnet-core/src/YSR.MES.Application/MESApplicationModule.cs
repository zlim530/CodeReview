using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YSR.MES.Authorization;
using Abp.Runtime.Caching.Redis;
using Microsoft.Extensions.Configuration;
using System;
using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.Configuration.Startup;

namespace YSR.MES
{
    [DependsOn(
        typeof(MESCoreModule), 
        typeof(AbpAutoMapperModule),
        typeof(AbpRedisCacheModule))] // 模块依赖于 Redis 缓存
    public class MESApplicationModule : AbpModule
    {
        private readonly IConfiguration _configuration;

        public MESApplicationModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 预加载
        /// </summary>
        public override void PreInitialize()
        {
            // 使用权限管理类
            Configuration.Authorization.Providers.Add<MESAuthorizationProvider>();


            #region using redisCache
            // 配置使用 Redis 缓存
            Configuration.Caching.UseRedis(option =>
            {
                option.ConnectionString = _configuration.GetSection("RedisCache:ConnectionString").Value ?? "127.0.0.1";
                int.TryParse(_configuration.GetSection("RedisCache:DatabaseId").Value, out var databaseId);
                option.DatabaseId = databaseId;
            });

            // 配置所有的 Cache 默认过期时间为30天
            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromDays(30);
            });

            // 设置指定缓存的默认过期时间为2小时，根据第一个参数 "CacheName" 来区分
            //Configuration.Caching.Configure("xxx", cache =>
            //{
            //    cache.DefaultSlidingExpireTime = TimeSpan.FromHours(2);
            //});

            #endregion


            Configuration.ReplaceService(typeof(AbpExceptionFilter), () =>
            {
                IocManager.Register<AbpExceptionFilter>(Abp.Dependency.DependencyLifeStyle.Transient);
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MESApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = false;

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
