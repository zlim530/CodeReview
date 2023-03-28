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
// ���ù�����
builder.Services.Configure<MvcOptions>(opt => {
    opt.Filters.Add<UnitOfWorkActionFilter>();
});
// ���� MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
//});

builder.Services.AddStackExchangeRedisCache(opt => {
    opt.Configuration = "localhost";
    opt.InstanceName = "UserMgrDemo_";// �Զ���һ��ʵ������������ redis ���������Ѵ��ڵ����ݻ����ͻ
});
// Ӧ�ò���з����ƴװ����������Ҫ�õ�ʲô����
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
