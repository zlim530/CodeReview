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