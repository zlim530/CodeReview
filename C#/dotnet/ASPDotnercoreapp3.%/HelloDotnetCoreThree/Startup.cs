using HelloDotnetCoreThree.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
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
                /*
                 UseDeveloperExceptionPage 中间件：如果存在异常并且环境是 Development，此中间件会被调用，显示开发异常页面
                 */

            }


            Console.WriteLine($"{env.WebRootPath}");
            //E:\localcode\CodeReview\C#\dotnet\HelloDotnetCoreThree\wwwroot

            /* 
            使用 Approach 1 显示的中间件样式是终端中间件，之所以称之为终端中间件，是因为它执行匹配的操作：
                ·前面示例中的匹配操作是用于中间件的 Path == "/" 和用于路由的 Path == "Movie"。
                ·如果匹配成功，它将执行一些功能并返回，而不是调用 next 中间件。
            之所以称之为终端中间件，是因为它会终止搜索，执行一些功能，然后返回。
             */
            // Approach 1:Writing a terminal middleware.
            /*app.Use(next => async context => {
                if (context.Request.Path == "/")
                {
                    await context.Response.WriteAsync("Hello terminal middleware!");
                    return;
                }

                await next(context);
            });*/

            // 注意：代码中的中间件注册顺序是很重要的，它代表了一个请求在管道中相继被各个中间件处理的顺序
            // 在代码中越早注册，表示越早被这种中间件处理

            // 注册了访问静态文件的中间件：添加静态文件中间件
            app.UseStaticFiles();


            // 路由中间件：向中间件管道添加路由匹配，此中间件会查看应用中定义的终结点集，并根据请求选择最佳匹配
            // Mathes request to an endpoint
            app.UseRouting();

            // Endpoint aware middleware.
            // Middleware can use metadata from the matched endpoint.
            //app.UseAuthentication();
            //app.UseAuthorization();
            /* 
            调用 UseAuthentication 和 UseAuthorization 会添加身份验证和授权中间件。
            这些中间件位于 UseRouting 和 UseEndpoints 之间，因此它们可以：
                ·查看 UseRouting 选择的终结点
                ·将 UseEndpoints 发送到终结点之前应用授权策略。
             */

            // Execute the matched endpoint.
            // 向中间件管道添加终结点执行，它会运行与所选终结点关联的委托
            app.UseEndpoints(endpoints => {
                /*endpoints.MapGet("/", async context => {
                    // Configure another endpoint,no authorization requirements.
                    await context.Response.WriteAsync("Hello World!");
                });*/
                /* 
                当 HTTP GET 请求发送到跟 URL / 时：
                    ·将执行显示的请求委托
                    ·Hello World！会写入 HTTP 响应。默认请求下，根 URL / 为：https://localhost:5001/
                如果请求方法不是 GET 或者根 URL 不是 /，则无路由匹配，并返回 HTTP 404
                 */

                /*endpoints.MapGet("/hello/{name:alpha}", async context =>
                {
                    var name = context.Request.RouteValues["name"];
                    await context.Response.WriteAsync($"Hello {name}!");
                });*/
                /* 
                /hello/{name:alpha} 字符串是一个路由模板，用于配置终结点的匹配方式。在这种情况下，模板将匹配：
                    ·类似 /hello/Ryan 的 URL
                    ·以 /hello/ 开头、后跟一系列字母字符的任何 URL 路径。:alpha 应用仅匹配字母字符的路由约束。
                URL 路径的第二段 {name:alpha}：
                    ·绑定到 name 参数
                    ·捕获并存储在 HttpRequest.RouteValues 中。
                 */


                // Configure the Health Check endpoint and require an authorized uses.
                //endpoints.MapHealthChecks("/healthz").RequireAuthorization();

                // Configure another endpoint,no authorization requirements.
                /*endpoints.MapGet("/",async context => {
                    await context.Response.WriteAsync("Hello World!");
                });*/

                /* 
                blog上述代码中的路由是专用的传统路由。 这称为专用的传统路由，因为：
                    ·它使用 传统路由。
                    ·它专用于特定的 操作。
                因为 controller 和 action 不会以参数形式出现在路由模板中 "blog/{*article}" ：
                    ·它们只能具有默认值 { controller = "Blog", action = "Article" } 。
                    ·此路由始终映射到操作 BlogController.Article 。
                /Blog、 /Blog/Article 和 /Blog/{any-string} 是唯一与博客路由匹配的 URL 路径。
                上面的示例：
                    ·blog 路由具有比路由更高的优先级， default 因为它是首先添加的。
                    ·是一个 缩略 名称样式路由的示例，在此示例中，通常将项目名称作为 URL 的一部分。
                 */
                /*endpoints.MapControllerRoute(
                    name: "blog",
                    pattern: "blog/{*article}",
                    defaults: new { controller = "Blog", action = "Arctile" }
                );*/

                /* 
                MapControllerRoute 用于创建单个路由。单路由命名为 default。大多数具有控制器和视图的应用都使用类似于路由的路由模板 default。REST Api 应使用属性路由。
                路由模板 "{controller=Home}/{action=Index}/{id?}"
                    ·匹配URL路径，例如 /Products/Details/5
                    ·{ controller = Products, action = Details, id = 5}通过词汇切分路径来提取路由值，如果应用有一个名为 ProductsController 的控制器和一个操作，则提取路由值将导致匹配 Details
                */
                // 注册了一个路由模板
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Department}/{action=Index}/{id?}"
                );
                /* 
                是一种传统路由。 它被称为 传统路由 ，因为它建立了一个 URL 路径 约定 ：
                    ·第一个路径段 {controller=Home} 映射到控制器名称。
                    ·第二段 {action=Index} 映射到 操作 名称。
                    ·第三段 {id?} 用于可选 id 。 ?中的 {id?} 使其成为可选的。 id 用于映射到模型实体。
                使用此 default 路由，URL 路径：
                    ·/Products/List 映射到 ProductsController.List 操作。
                    ·/Blog/Article/17 映射到 BlogController.Article 和通常将参数绑定 id 到17。
                此映射：
                    ·仅基于控制器和操作名称。
                    ·不基于命名空间、源文件位置或方法参数。
                    */


                //endpoints.MapControllers();
                // REST Api 的属性路由

            });
            /*
            我们只能将一个终端中间件添加到请求管道。
            终端中间件是我们之前已经说到过，他会使管道短路，不会去调用下一个中间件。
             */
        }
    }
}
