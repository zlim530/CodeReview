using SignalRDemo.Helpers;
using SignalRDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR().AddStackExchangeRedis("127.0.0.1",opt => { 
    opt.Configuration.ChannelPrefix= "WebApp_SignalR_";// ʹ�� Redis ����Ϣ������ʵ�� SignalR �ķֲ�ʽ����
    // ������ͬ�� SignalR �������˿��Ի���ͨ�ţ�����Ⱥ����Ϣʱ���и��������������Ŀͻ��˶����Խ��յ���Ϣ
});

// ���� Cors 
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(opt =>
        opt.AddDefaultPolicy(b =>
            b.WithOrigins(urls)
            .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
        )
    );

builder.Services.AddSingleton<ImportExecutor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ���� CORS
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ImportHub>("/Hubs/MyHub");

app.MapControllers();

app.Run();
