namespace MiddleWareDemo;

/// <summary>
/// 封装中间件类，不需要继承任何接口只需要有一个接收 RequestDelegate 参数的构造函数与有一个参数为 HttpContext 类型返回值为 Task 的 Invoke(Async) 方法即可
/// </summary>
public class TestMiddleware 
{
    private readonly RequestDelegate next;

    public TestMiddleware(RequestDelegate next)
    {
        this.next = next; 
    }

    public async Task InvokeAsync(HttpContext context)
    { 
        context.Response.WriteAsync("TestMiddleware start <br/>");
        await next.Invoke(context);
        context.Response.WriteAsync("TestMiddleware end <br/>");
    }
}