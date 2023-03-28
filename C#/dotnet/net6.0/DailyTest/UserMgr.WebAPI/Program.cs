using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserMgr.Domain;
using UserMgr.Domain.Interfaces;
using UserMgr.Infrastracture;
using UserMgr.Infrastracture.DbContexts;
using UserMgr.WebAPI.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(opt => {
    opt.UseSqlServer("Server=127.0.0.1;Database=DemoDDD;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;");
});
// 配置过滤器
builder.Services.Configure<MvcOptions>(opt => {
    opt.Filters.Add<UnitOfWorkActionFilter>();
});
// 配置 MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
//});

builder.Services.AddStackExchangeRedisCache(opt => {
    opt.Configuration = "localhost";
    opt.InstanceName = "UserMgrDemo_";// 自定义一个实例名，避免与 redis 服务器中已存在的数据缓存冲突
});
// 应用层进行服务的拼装：来决定需要用到什么服务
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserDomainRepository, UserDomainRepository>();
builder.Services.AddScoped<ISmsCodeSender, MockSmsCodeSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
