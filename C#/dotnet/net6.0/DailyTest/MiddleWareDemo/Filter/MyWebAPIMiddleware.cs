using System.Text.Json;

namespace MiddleWareDemo;

public class MyWebAPIMilldeware
{
    private readonly RequestDelegate next;
    private readonly ActionLocator actionLocator;

    public MyWebAPIMilldeware(RequestDelegate next, ActionLocator actionLocator)
    {
        this.next = next;
        this.actionLocator = actionLocator;
    }

    /// <summary>
    /// 在运行的时候才能拿到能用的IServiceProvider
    /// 所以IServiceProvider不通过构造函数注入
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sp"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, IServiceProvider sp)
    {
        (bool ok, string? ctrName, string? actionName) = PathParser.Parse(context.Request.Path);
        if (ok == false)
        {
            await next(context);
            return;
        }
        // 使用控制器的名字和操作方法的名字来加载控制器方法对应 MethodInfo 类型的对象
        var actionMethod = actionLocator.LocateActionMethod(ctrName!, actionName!);
        if (actionMethod == null)
        {
            await next(context);
            return;
        }
        Type controllerType = actionMethod.DeclaringType!;
        object controllerInstance = sp.GetRequiredService(controllerType);
        var paraValues = BindingHelper.GetParameterValues(context, actionMethod);
        // 如果有过滤器，在执行 Action 前先执行 Filter
        // 中间件与 Filter 的区别：中间件是 ASP.NETCore 框架提供的基础服务功能，而 Filter 则是中间件中内部的响应处理
        // 也即中间件是包含 Filter 的，Filter 属于中间件中提供的功能
        // 中间件可以处理所有的请求，而 Filter 只能处理针对控制器的请求：中间件运行在一个更底层、更抽象的级别，因此在中间件中无法处理 MCV 框架特有的概念
        // 中间件和 Filter 可以完成很多相似的功能：“未处理异常中间件”和“未处理异常 Filter”；“请求限流中间件”和“请求限流 Filter”的区别
        // 建议优先使用中间件：但是如果这个组件只针对 MVC 或者需要调用一些 MVC 相关的类时，就只能选择 Filter 了
        foreach (var filter in ActionFilters.Filters)
        {
            filter.Execute();
        }
        var result = actionMethod.Invoke(controllerInstance, paraValues);
        // 限定返回值只能是普通类型，而不能是 IActionResult 等
        var jsonStr = JsonSerializer.Serialize(result);
        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json; charset=utf-8";
        await context.Response.WriteAsync(jsonStr);
    }

}