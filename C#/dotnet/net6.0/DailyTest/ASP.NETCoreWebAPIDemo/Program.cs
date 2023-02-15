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

            // 注册自定义服务
            //builder.Services.AddScoped<MyServices>();
            //// 注册耗时任务服务
            //builder.Services.AddScoped<LongTimeServices>();

            // 获取所有程序集中实现了 IModuleInitializer 接口的程序类
            // 这样就不需要再服务调用程序中手动的注册服务，而是只需要在封装的 Initialize() 方法中注册即可
            var asms = ReflectionHelper.GetAllReferencedAssemblies();
            builder.Services.RunModuleInitializers(asms);

            string[] urls = new[] { "http://localhost:5173" };
            builder.Services.AddCors(opt => 
                    opt.AddDefaultPolicy(b => 
                        b.WithOrigins(urls)
                        .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                    )
                );

            var app = builder.Build();

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