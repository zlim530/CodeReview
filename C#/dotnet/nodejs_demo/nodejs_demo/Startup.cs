using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace nodejs_demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 使用扩展类创建的静态方法来配置服务：
            services.ConfigureCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "nodejs_demo", Version = "v1" });
            });

            #region 使用 JWT Using JWT
            // 1.在 ConfigureServices 中配置 JWT 认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        //ValidIssuer = _authToken.Issuer,

                        ValidateAudience = true,
                        //ValidAudience = _authToken.Audience,

                        ValidateIssuerSigningKey = true,
                        //IssuerSigningKey = new AsymmetricSecurityKey(Encoding.UTF8.GetBytes(_authToken.Key));

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        };
                });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region 处理全局异常 Handling Errors Globally
            // 1.在 Startup 类中修改 Configure 方法：
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        await context.Response.WriteAsync(new ErrorModel
                        {
                            StatusCode = 500,
                            ErrorMessage = ex.Message
                        }.ToString());
                    }
                });
            });

            // 2.创建自定义的中间件来实现我们的自定义异常处理：见 CustomExceptionMiddleware 文件

            // 3.将自定义的异常处理中间件注入到应用程序的请求管道中即可：
            app.UseCutomeExceptionMiddleware();
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "nodejs_demo v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            // 2.使用 JWT
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
