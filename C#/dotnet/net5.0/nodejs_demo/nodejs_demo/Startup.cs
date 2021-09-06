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
            // ʹ����չ�ഴ���ľ�̬���������÷���
            services.ConfigureCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "nodejs_demo", Version = "v1" });
            });

            #region ʹ�� JWT Using JWT
            // 1.�� ConfigureServices ������ JWT ��֤
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
            #region ����ȫ���쳣 Handling Errors Globally
            // 1.�� Startup �����޸� Configure ������
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

            // 2.�����Զ�����м����ʵ�����ǵ��Զ����쳣������ CustomExceptionMiddleware �ļ�

            // 3.���Զ�����쳣�����м��ע�뵽Ӧ�ó��������ܵ��м��ɣ�
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

            // 2.ʹ�� JWT
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
