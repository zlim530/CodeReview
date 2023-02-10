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

            // ע���Զ������
            //builder.Services.AddScoped<MyServices>();
            //// ע���ʱ�������
            //builder.Services.AddScoped<LongTimeServices>();

            // ��ȡ���г�����ʵ���� IModuleInitializer �ӿڵĳ�����
            // �����Ͳ���Ҫ�ٷ�����ó������ֶ���ע����񣬶���ֻ��Ҫ�ڷ�װ�� Initialize() ������ע�ἴ��
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}