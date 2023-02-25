namespace MiddleWareDemo;

/// <summary>
/// ��װ�м���࣬����Ҫ�̳��κνӿ�ֻ��Ҫ��һ������ RequestDelegate �����Ĺ��캯������һ������Ϊ HttpContext ���ͷ���ֵΪ Task �� Invoke(Async) ��������
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