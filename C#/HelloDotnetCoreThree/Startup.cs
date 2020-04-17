using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloDotnetCoreThree.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloDotnetCoreThree {
    // Startup 类用于配置服务和应用的请求管道(在 Configure 方法中配置着管道中的多种中间件)
    public class Startup {
        
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            // DI：dependency injection 依赖注入
            // IoC：inversion of control 控制反转
            // 注册依赖：交给 IoC 容器来管理类
            // Singleton:在整个 web 程序应用服务期间只创建一个实例对象
            // 这里泛型参数的意思是：接收一个 IClock 类型的对象，并返回一个 ChinaClock 类型的对象
            services.AddSingleton<IClock,ChinaClock>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            // 判断服务的执行环境
            // 执行环境（例如 Development、Staging 和 Production）是 ASP.NET Core 中的高级概念。 
            // 通过设置 ASPNETCORE_ENVIRONMENT 环境变量来指定应用的运行环境。
            // ASP.NET Core 在应用启动时读取该环境变量，并将该值存储在 IWebHostEnvironment 实现中。 通过依赖关系注入 (DI)，可以在应用中任何位置实现此操作
            if (env.IsDevelopment()) {// 是否为开发环境
                // 只有为开发环境时才会显示页面异常的详细信息
                // 而为生产环境( Production )时不建议把这种异常的详细信息也就是内部实现细节展示给客户
                app.UseDeveloperExceptionPage();
            }

            // 注意：代码中的中间件注册顺序是很重要的，它代表了一个请求在管道中相继被各个中间件处理的顺序
            // 在代码中越早注册，表示越早被这种中间件处理

            // 注册了访问静态文件的中间件
            app.UseStaticFiles();

            app.UseAuthentication();

            // 路由中间件
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                //endpoints.MapGet("/", async context => {
                //    await context.Response.WriteAsync("Hello World!");
                //});
                // 注册了一个路由模板
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
