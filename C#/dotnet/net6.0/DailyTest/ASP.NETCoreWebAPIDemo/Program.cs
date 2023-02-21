using ASP.NETCoreWebAPIDemo.Model;
using StackExchange.Redis;
using System.Data.SqlClient;
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

            var app = builder.Build();

            Console.WriteLine(app.Environment.EnvironmentName);// 读取 ASP .NET Core API 中的默认配置的环境变量
            Console.WriteLine(app.Environment.IsDevelopment());// 扩展方式：实际上就是判断 EnvironmentName 的值
            Console.WriteLine(app.Environment.IsProduction());
            Console.WriteLine(app.Configuration.GetSection("connStr").Value);

            var webBuilder = builder.Host;
            webBuilder.ConfigureAppConfiguration((hostCtx, configBuilder) =>
            {
                //var configRoot = builder.Configuration;
                //string connStr = configRoot.GetConnectionString("connStr");
                var connStr = app.Configuration.GetSection("connStr").Value;
                configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp => {
                // 在 Program.cs 中读取配置的一种方法：直接使用 builder.Configuration
                var constr = builder.Configuration.GetSection("Redis").Value;
                return ConnectionMultiplexer.Connect(constr);
            });
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

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