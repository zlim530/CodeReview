using FluentValidation.AspNetCore;
using IdentitySeverDemo.DbContext;
using IdentitySeverDemo.Filter;
using IdentitySeverDemo.HostService;
using IdentitySeverDemo.Hubs;
using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// 配置 OpenAPI ：让 Swagger 界面中带上 JWT 报文头请求接口
builder.Services.AddSwaggerGen(c => {
    var schema = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample:'Bearer 123456abcdf'",
        Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization" },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header, Type = SecuritySchemeType.ApiKey
    };
    c.AddSecurityDefinition("Authorization", schema);
    var requirement = new OpenApiSecurityRequirement();
    requirement[schema] = new List<string>();
    c.AddSecurityRequirement(requirement);
});

builder.Services.Configure<MvcOptions>(opt => {
    opt.Filters.Add<JWTVersionCheckFilter>();
});

builder.Services.AddHostedService<ScheduledHostService>();

builder.Services.AddFluentValidation(opt => {
    opt.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());// 加载当前程序集
});

builder.Services.AddDbContext<MyDbContext>(opt => {
    var connStr = builder.Configuration.GetSection("connStr").Value;
    opt.UseSqlServer(connStr);
});

// 配置 JWT
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        var jwtSettings = builder.Configuration.GetSection("JWt").Get<JWTSettings>();
        var bytes = Encoding.UTF8.GetBytes(jwtSettings.SecKey);
        var secKey = new SymmetricSecurityKey(bytes);
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secKey
        };
        // 在 JWT 中配置 SignalR 
        opt.Events = new JwtBearerEvents { 
            OnMessageReceived = context => {
                // 因为 websocket 不支持自定义报文头，因此我们需要把 JWT 通过 url 中的 QueryString 传递
                // 然后在服务器端的 OnMessageReceived 中，把 QueryString 中的 JWT 读出来赋值给 context.Token
                var accessToken = context.Request.Query["acess_token"];
                var path = context.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Hubs/MyHub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
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
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
IdentityBuilder identityBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
identityBuilder.AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders().AddUserManager<UserManager<MyUser>>().AddRoleManager<RoleManager<MyRole>>();

// 启用 Cors 
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(opt =>
        opt.AddDefaultPolicy(b =>
            b.WithOrigins(urls)
            .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
        )
    );

builder.Services.AddSignalR().AddStackExchangeRedis("127.0.0.1", opt => {
    opt.Configuration.ChannelPrefix = "WebApp_SignalR_";// 使用 Redis 的消息队列来实现 SignalR 的分布式部署
    // 这样不同的 SignalR 服务器端可以互相通信，这样群发消息时所有跟服务器端相连的客户端都可以接收到消息
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 启用 CORS：让前端可以访问后端 SignalR 服务器
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<MyHub>("/Hubs/MyHub");

app.MapControllers();

app.Run();
