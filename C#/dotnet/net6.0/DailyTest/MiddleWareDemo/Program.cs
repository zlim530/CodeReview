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


#region 中间件的基本使用与中间件类
/*app.Map("/test", async (pipeBuilder) => {
    pipeBuilder.UseMiddleware<CheckAndParsingMiddleware>();

    pipeBuilder.Use(async (context, next) => {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 start <br/>");
        // 注意：一般不要在中间件的 Use 方法中输出内容，因为可能导致 Http Response 发生混乱，一般是在最后的 Run 方法中进行输出，这里只是初始尝试中间件进行 Demo 测试
        await next.Invoke();
        await context.Response.WriteAsync("1 end <br/>");
    });

    pipeBuilder.Use(async (context, next) => {
        await context.Response.WriteAsync("2 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("2 end <br/>");
    });

    pipeBuilder.UseMiddleware<TestMiddleware>();// 使用中间件类：复用中间件逻辑

    pipeBuilder.Run(async context => {
        await context.Response.WriteAsync("Run <br/>");
        dynamic? obj = context.Items["BodyJson"];
        if (obj != null)
        {
            await context.Response.WriteAsync($"{obj} <br/>");
        }
    });
    
    *//*
    Run 表示中间件的终点，在 Run 方法之后的 Use 代码将不会执行
    pipeBuilder.Use(async (context, next) => {
        await context.Response.WriteAsync("3 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("3 end <br/>");
    });*//*
});*/
#endregion

app.Run();
