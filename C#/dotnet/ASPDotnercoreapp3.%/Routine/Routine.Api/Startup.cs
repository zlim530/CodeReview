using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Routine.Api.Data;
using Routine.Api.Services;

namespace Routine.Api {
    public class Startup {
        // 构造函数注入 IConfiguration 
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddDbContext<RoutineDbContext>(options => {
                //options.UseSqlite("Data Source=routine.db");
                options.UseSqlServer("Data Source=localhost;DataBase=routine;Integrated Security=true");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            /*
             路由机制会把一个请求的URL映射到一个Controller上面的Action，所以当你发送一个HTTP请求的时候，MVC框架会解析这个请求的URL，并尝试这把它映射到一个Controller上面的Action
             */
            app.UseRouting();// 用来标记路由决策在请求管道里发生的位置，也就是在这里会选择端点

            // 授权中间件：是一个非常好的例子
            //如果授权成功，那么就继续执行到之前选定的端点，否则的话会跳转到其他短或者短路返回。
            app.UseAuthorization();

            // 用来标记选择好的端点在请求中管道的什么地方来执行
            // 这样做的好处是，我们可以选择在选择端点和执行端点的中间位置插入其他的中间件。这样的话，插入到中间位置的中间件就会知道哪个端点被选取了，而且它也有可能会选择其他的端点
            // 针对Web API，使用基于属性的路由更加适合：
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                /*
                 这里面仅仅映射了Controller，并没有使用任何约定，所有我们需要采用属性（Attribute）来进行设定。这里需要用到
                属性（attribute）和URL模板
                ·属性（Attribute）：例如[Route]，[HttpGet]，[HttpPost]等等，可以把它们放在Controller级别，也可以放在Action级别上。
                ·URL模板：将属性结合URL模板一起使用，就可以把请求映射到Controller的Action上面
                 */
            });
        }
    }
}
