using SignalRDemo.Helpers;
using SignalRDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR().AddStackExchangeRedis("127.0.0.1",opt => { 
    opt.Configuration.ChannelPrefix= "WebApp_SignalR_";// 使用 Redis 的消息队列来实现 SignalR 的分布式部署
    // 这样不同的 SignalR 服务器端可以互相通信，这样群发消息时所有跟服务器端相连的客户端都可以接收到消息
});

// 启用 Cors 
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

// 启用 CORS
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ImportHub>("/Hubs/MyHub");

app.MapControllers();

app.Run();
