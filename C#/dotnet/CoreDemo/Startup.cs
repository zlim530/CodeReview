using CoreDemo.Services;
using CoreDemo.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreDemo {
    public class Startup {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        /*
        ConfigureServices 用来配置依赖注入的
        注入容器的生命周期：依赖注入，IoC容器
        - Transient 瞬态模式：每次服务被请求都会创建新的实例，服务在每次请求时被创建
        - Scoped 作用域模式：服务在每次请求时被创建，整个请求过程中都贯穿使用这个创建的服务；每次Web请求会创建一个实例
        - Singleton 单例模式：服务在第一次请求时被创建，其后的每次请求都沿用这个已创建的服务；一旦被创建实例，就会一直使用这个实例，直到应用停止，在整个 web 程序应用服务期间只创建一个实例对象
        */
        public void ConfigureServices(IServiceCollection services) {
            // 配置 MVC 
            /*
            如果是 netcore3.0 请这么写：services.AddMvc(options => options.EnableEndpointRouting = false);
            */
            services.AddMvc();

            // 如果请求 ICinemaService 类型，则会返回 CinemaMemoryService 类型的对象
            services.AddSingleton<ICinemaService, CinemaMemoryService>();
            services.AddSingleton<IMovieService,MovieMemoryService>();

            // 将 appsettings 配置文件中的 json 字符串与程序中的强类型实体类一一对应起来
            // 并且我们对应的是 appsettings 中的一部分 json 字符串，而并不是全部
            // 注册成功后，就可以在控制器或者视图中使用这个实体类的属性
            services.Configure<ConnectionOptions>(_configuration.GetSection("ConnectionStrings"));

        }

        // Configure 配置管道的，告诉服务器如何响应 HTTP 请求
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger) {
            // Development:开发环境 Staging：上线环境 Production：生产环境 也可以自己自定义环境变量
            if (env.IsDevelopment()) {
                // 在开发环境中,使用异常页面,这样可以暴露错误堆栈信息,故不要放在生产环境
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输（or HSTS）对于保护web安全是非常重要的
                // 强制实施 HTTPS 在 ASP .NET Core 配合 app.UseHTTPSRedirect
                //app.UseHsts();
            }

            /*如果需要发布上线可以设置：
            if (env.IsProduction()) {
                app.UseForwardedHeaders(new ForwardedHeadersOptions { 
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }*/

            app.Run(async context =>
            {
                logger.LogInformation("run 1 start");
                await context.Response.WriteAsync("Hello world!run 1.");
                logger.LogInformation("run 1 end.");
            });


            app.UseStatusCodePages();
            app.UseStaticFiles();

            // 配置 MVC 中间件
            app.UseMvc(routes => {
                // 配置一个最简单的路由
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            #region 项目初始化中间件配置
            /*app.Use(async (context, next) => {
                logger.LogInformation("M1 Start");
                await context.Response.WriteAsync("zlim,");
                await next();
                // 异步调用 next()，因此当执行此中间件时，next() 方法后面的代码会在 M2 End 后面执行
                logger.LogInformation("M1 End");
            });

            // 如果想使用下一个中间件，则不能使用 app.Run
            // 因为 app.Run 是终端中间件，会直接返回响应
            app.Run(async (context) => {
                logger.LogInformation("M2 Start");
                await context.Response.WriteAsync("Hello World!");
                logger.LogInformation("M2 End");
            });*/

            /*
            info: CoreDemo.Startup[0]
                  M1 Start
            info: CoreDemo.Startup[0]
                  M2 Start
            info: CoreDemo.Startup[0]
                  M2 End
            info: CoreDemo.Startup[0]
                  M1 End
            */
            #endregion
        }


    }

}
