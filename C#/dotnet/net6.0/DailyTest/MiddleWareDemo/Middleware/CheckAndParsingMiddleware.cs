using Dynamic.Json;
using System.Text.Json;

namespace MiddleWareDemo;

public class CheckAndParsingMiddleware
{
    /// <summary>
    /// RequestDelegate ��������Ĵ����������Ͻ�������д������մ��� EndPoint ָ��� Controller Action ������ί��
    /// </summary>
    private readonly RequestDelegate next;

    public CheckAndParsingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// ����������Ƿ��� password=123 �Ĳ�ѯ�ַ��������������������尴�� Json ��ʽ����Ϊ dynamic ���͵Ķ��󣬲��� dynamic ������� context.Items �й��������м������ Run ����ʹ��
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
                // �� System.Text.Json ��֮ǰ��֧�ְ� Json �����л�Ϊ dynamic ���Ͷ���ʱ����ʹ�� Dynamic.Json nuget ���е� DJson ����
                dynamic? jsonObj = await DJson.ParseAsync(reqStream);
                //dynamic? jsonObj = JsonSerializer.Deserialize<dynamic>(reqStream);
                // context.Items��������һ�� HTTP �����ڹ�������
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