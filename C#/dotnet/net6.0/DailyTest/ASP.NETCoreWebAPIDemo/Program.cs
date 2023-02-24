using ASP.NETCoreWebAPIDemo.Controllers;
using ASP.NETCoreWebAPIDemo.Filter;
using ASP.NETCoreWebAPIDemo.Model;
using EntityFrameworkCoreModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using Zack.ASPNETCore;
using Zack.Commons;

namespace ASP.NETCoreWebAPIDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache(); // 启用内存缓存
            builder.Services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
            builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();

            // 注册自定义服务
            //builder.Services.AddScoped<MyServices>();
            //// 注册耗时任务服务
            //builder.Services.AddScoped<LongTimeServices>();

            // 获取所有程序集中实现了 IModuleInitializer 接口的程序类
            // 这样就不需要再服务调用程序中手动的注册服务，而是只需要在封装的 Initialize() 方法中注册即可
            var asms = ReflectionHelper.GetAllReferencedAssemblies();
            builder.Services.RunModuleInitializers(asms);
            builder.Services.AddStackExchangeRedisCache(opt => {
                opt.Configuration = "localhost";
                opt.InstanceName = "cache1_";// 自定义一个实例名，避免与 redis 服务器中已存在的数据缓存冲突
            });

            string[] urls = new[] { "http://localhost:5173" };
            builder.Services.AddCors(opt => 
                    opt.AddDefaultPolicy(b => 
                        b.WithOrigins(urls)
                        .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                    )
                );

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                // 在 Program.cs 中读取配置的一种方法：直接使用 builder.Configuration
                var constr = builder.Configuration.GetSection("Redis").Value;
                return ConnectionMultiplexer.Connect(constr);
            });
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

            // Scoped：仅在一次 HTTP(S) 请求中维持
            //builder.Services.AddDbContext<MyMultiDBContext>(opt => {
            //    var connStr = builder.Configuration.GetSection("connStr").Value;
            //    opt.UseSqlServer(connStr);
            //});
            //// 因为默认是 Scope，每次 http 请求就要重新创建 DbContext，当 DbContext 中有很多个表时会影响性能，所以把多个 DbSet 分开设置到多个 DbContext 中
            //builder.Services.AddDbContext<MyMovieDBContext>(opt => {
            //    var connStr = builder.Configuration.GetSection("connStr").Value;
            //    opt.UseSqlServer(connStr);
            //});
            // 优雅的注入多个 DbContext 对象：
            //var arms = new Assembly[] { Assembly.Load("EntityFrameworkCoreModel") };
            var arms = ReflectionHelper.GetAllReferencedAssemblies();
            builder.Services.AddAllDbContexts(opt => {
                var connStr = builder.Configuration.GetSection("connStr").Value;
                opt.UseSqlServer(connStr);
            }, asms);

            builder.Services.Configure<MvcOptions>(opt => {
                // 异常服务类的注册顺序有关系，后注册的会先执行，如果最后注册的异常处理类中设置了 context.ExceptionHandled = true 那么将导致在它之前注册的异常实现类不会被执行
                // 如果需要执行其他异常服务类，可以在其后面注册或者不再异常处理类中设置 context.ExceptionHandled = true
                opt.Filters.Add<RateLimitFilter>();// 将 API 限流过滤器放在最前面，保证如果不符合要求的请求就不会再继续经过其他过滤器
                opt.Filters.Add<LogExceptionFilter>();
                opt.Filters.Add<MyExceptionFilter>();
                opt.Filters.Add<MyActionFilterTest1>();
                opt.Filters.Add<MyActionFilterTest2>();
                opt.Filters.Add<TransactionScopeFilter>();
            });

            var app = builder.Build();

            Console.WriteLine(app.Environment.EnvironmentName);// 读取 ASP .NET Core API 中的默认配置的环境变量
            Console.WriteLine(app.Environment.IsDevelopment());// 扩展方式：实际上就是判断 EnvironmentName 的值
            Console.WriteLine(app.Environment.IsProduction());
            Console.WriteLine(app.Configuration.GetSection("connStr").Value);

            //var webBuilder = builder.Host;
            //webBuilder.ConfigureAppConfiguration((hostCtx, configBuilder) =>
            //{
            //    //var configRoot = builder.Configuration;
            //    //string connStr = configRoot.GetConnectionString("connStr");
            //    var connStr = app.Configuration.GetSection("connStr").Value;
            //    configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
            //});

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            //app.UseResponseCaching(); // 启用服务器端响应缓存：比较鸡肋，跟客户端缓存一样可以在请求头在加入禁用缓存来解除从缓存中读取数据

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}