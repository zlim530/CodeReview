using IdentitySeverDemo.DbContext;
using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(opt => {
    var connStr = builder.Configuration.GetSection("connStr").Value;
    opt.UseSqlServer(connStr);
});
builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<MyUser>(options => {
    /*
    账户锁定设置：
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromDays(1);*/

    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase= false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase= false;
    // 如果需要用户输入验证码，则配置为以下配置，这样生成的验证码是6位数字
    // 如果不做设置，则生成的生成的重置密码 token 会非常非常长，这这种很长很复杂的验证码适合发送到用户邮箱，让ta点击包含验证码的链接
    // CfDJ8B7DtV0qrQFGv8wHiQKh7EIS2g2aNQTHa7ryLJx2qKlil8JbPprl75nn8gLrCoUNdyH/wRGFE/Bg/qadRg8xKMWmEhE9WlTq1PQSJGYOooqvCBlSn4qGKCKU/vCWjLaOyB2YbsRWMWa0kobfKzrQAqFq/Z0s9gKs41hSRhkTnzx1dgE+BzHRS6VQOXcq/eTpcQ==
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
IdentityBuilder identityBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
identityBuilder.AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders().AddUserManager<UserManager<MyUser>>().AddRoleManager<RoleManager<MyRole>>();

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
