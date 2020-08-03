using CoreDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreDemo {
    public class Startup {
        /*
        ConfigureServices 用来配置依赖注入的
        注入容器的声明周期：依赖注入，IoC容器
        - Transient：每次被请求都会创建新的实例
        - Scoped：每次Web请求会创建一个实例
        - Singleton：一旦被创建实例，就会一直使用这个实例，直到应用停止
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

        }

        // Configure 配置管道的，告诉服务器如何响应 HTTP 请求
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger) {
            // Development:开发环境 Staging：上线环境 Production：生产环境 也可以自己自定义环境变量
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

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
