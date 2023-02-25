namespace MiddleWareDemo;

public class NotFoundMilldeware
{
    private readonly RequestDelegate next;

    public NotFoundMilldeware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.StatusCode = 404;
        context.Response.ContentType= "text/htmll;charset=utf-8";
        await context.Response.WriteAsync("请求来到了一片未知的荒原！");
    }
}