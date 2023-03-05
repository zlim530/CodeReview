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
// ���� OpenAPI ���� Swagger �����д��� JWT ����ͷ����ӿ�
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
    opt.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());// ���ص�ǰ����
});

builder.Services.AddDbContext<MyDbContext>(opt => {
    var connStr = builder.Configuration.GetSection("connStr").Value;
    opt.UseSqlServer(connStr);
});

// ���� JWT
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
        // �� JWT ������ SignalR 
        opt.Events = new JwtBearerEvents { 
            OnMessageReceived = context => {
                // ��Ϊ websocket ��֧���Զ��屨��ͷ�����������Ҫ�� JWT ͨ�� url �е� QueryString ����
                // Ȼ���ڷ������˵� OnMessageReceived �У��� QueryString �е� JWT ��������ֵ�� context.Token
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
    �˻��������ã�
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromDays(1);*/

    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase= false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase= false;
    // �����Ҫ�û�������֤�룬������Ϊ�������ã��������ɵ���֤����6λ����
    // ����������ã������ɵ����ɵ��������� token ��ǳ��ǳ����������ֺܳ��ܸ��ӵ���֤���ʺϷ��͵��û����䣬��ta���������֤�������
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
IdentityBuilder identityBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
identityBuilder.AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders().AddUserManager<UserManager<MyUser>>().AddRoleManager<RoleManager<MyRole>>();

// ���� Cors 
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(opt =>
        opt.AddDefaultPolicy(b =>
            b.WithOrigins(urls)
            .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
        )
    );

builder.Services.AddSignalR().AddStackExchangeRedis("127.0.0.1", opt => {
    opt.Configuration.ChannelPrefix = "WebApp_SignalR_";// ʹ�� Redis ����Ϣ������ʵ�� SignalR �ķֲ�ʽ����
    // ������ͬ�� SignalR �������˿��Ի���ͨ�ţ�����Ⱥ����Ϣʱ���и��������������Ŀͻ��˶����Խ��յ���Ϣ
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ���� CORS����ǰ�˿��Է��ʺ�� SignalR ������
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<MyHub>("/Hubs/MyHub");

app.MapControllers();

app.Run();
