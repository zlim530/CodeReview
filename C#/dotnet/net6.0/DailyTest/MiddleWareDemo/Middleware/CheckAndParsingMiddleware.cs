using Dynamic.Json;
using System.Text.Json;

namespace MiddleWareDemo;

public class CheckAndParsingMiddleware
{
    /// <summary>
    /// RequestDelegate 就是请求的处理器，不断将请求进行处理，最终传到 EndPoint 指向的 Controller Action 方法的委托
    /// </summary>
    private readonly RequestDelegate next;

    public CheckAndParsingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// 检查请求中是否有 password=123 的查询字符串，如果有则把请求报文体按照 Json 格式解析为 dynamic 类型的对象，并把 dynamic 对象放入 context.Items 中供后续的中间件或者 Run 方法使用
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        string pwd = context.Request.Query["password"];
        if (pwd == "123")
        {
            if (context.Request.HasJsonContentType())
            {
                var reqStream = context.Request.BodyReader.AsStream();
                // 当 System.Text.Json 在之前不支持把 Json 反序列化为 dynamic 类型对象时可以使用 Dynamic.Json nuget 包中的 DJson 方法
                dynamic? jsonObj = await DJson.ParseAsync(reqStream);
                //dynamic? jsonObj = JsonSerializer.Deserialize<dynamic>(reqStream);
                // context.Items：用于在一次 HTTP 请求内共享数据
                context.Items["BodyJson"] = jsonObj;
            }
            await next(context);
        }
        else
        {
            context.Response.StatusCode = 401;
        }
    }
}