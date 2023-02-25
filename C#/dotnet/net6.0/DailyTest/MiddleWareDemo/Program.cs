using MiddleWareDemo;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
ActionLocator locator = new ActionLocator(services, Assembly.GetEntryAssembly()!);
services.AddSingleton(locator);
services.AddMemoryCache();
ActionFilters.Filters.Add(new MyActionFilter());
var app = builder.Build();


app.UseMiddleware<MyStaticFileMiddleware>();
app.UseMiddleware<MyWebAPIMilldeware>();
app.UseMiddleware<NotFoundMilldeware>();


//app.MapGet("/", () => "Hello World!");


#region �м���Ļ���ʹ�����м����
/*app.Map("/test", async (pipeBuilder) => {
    pipeBuilder.UseMiddleware<CheckAndParsingMiddleware>();

    pipeBuilder.Use(async (context, next) => {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 start <br/>");
        // ע�⣺һ�㲻Ҫ���м���� Use ������������ݣ���Ϊ���ܵ��� Http Response �������ң�һ���������� Run �����н������������ֻ�ǳ�ʼ�����м������ Demo ����
        await next.Invoke();
        await context.Response.WriteAsync("1 end <br/>");
    });

    pipeBuilder.Use(async (context, next) => {
        await context.Response.WriteAsync("2 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("2 end <br/>");
    });

    pipeBuilder.UseMiddleware<TestMiddleware>();// ʹ���м���ࣺ�����м���߼�

    pipeBuilder.Run(async context => {
        await context.Response.WriteAsync("Run <br/>");
        dynamic? obj = context.Items["BodyJson"];
        if (obj != null)
        {
            await context.Response.WriteAsync($"{obj} <br/>");
        }
    });
    
    *//*
    Run ��ʾ�м�����յ㣬�� Run ����֮��� Use ���뽫����ִ��
    pipeBuilder.Use(async (context, next) => {
        await context.Response.WriteAsync("3 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("3 end <br/>");
    });*//*
});*/
#endregion

app.Run();
