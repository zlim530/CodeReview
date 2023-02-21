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
            builder.Services.AddMemoryCache(); // �����ڴ滺��
            builder.Services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
            builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();

            // ע���Զ������
            //builder.Services.AddScoped<MyServices>();
            //// ע���ʱ�������
            //builder.Services.AddScoped<LongTimeServices>();

            // ��ȡ���г�����ʵ���� IModuleInitializer �ӿڵĳ�����
            // �����Ͳ���Ҫ�ٷ�����ó������ֶ���ע����񣬶���ֻ��Ҫ�ڷ�װ�� Initialize() ������ע�ἴ��
            var asms = ReflectionHelper.GetAllReferencedAssemblies();
            builder.Services.RunModuleInitializers(asms);
            builder.Services.AddStackExchangeRedisCache(opt => {
                opt.Configuration = "localhost";
                opt.InstanceName = "cache1_";// �Զ���һ��ʵ������������ redis ���������Ѵ��ڵ����ݻ����ͻ
            });

            string[] urls = new[] { "http://localhost:5173" };
            builder.Services.AddCors(opt => 
                    opt.AddDefaultPolicy(b => 
                        b.WithOrigins(urls)
                        .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                    )
                );

            var app = builder.Build();

            Console.WriteLine(app.Environment.EnvironmentName);// ��ȡ ASP .NET Core API �е�Ĭ�����õĻ�������
            Console.WriteLine(app.Environment.IsDevelopment());// ��չ��ʽ��ʵ���Ͼ����ж� EnvironmentName ��ֵ
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
                // �� Program.cs �ж�ȡ���õ�һ�ַ�����ֱ��ʹ�� builder.Configuration
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

            //app.UseResponseCaching(); // ���÷���������Ӧ���棺�Ƚϼ��ߣ����ͻ��˻���һ������������ͷ�ڼ�����û���������ӻ����ж�ȡ����

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}