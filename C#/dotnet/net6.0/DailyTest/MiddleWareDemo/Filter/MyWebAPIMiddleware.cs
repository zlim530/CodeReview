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
    /// �����е�ʱ������õ����õ�IServiceProvider
    /// ����IServiceProvider��ͨ�����캯��ע��
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
        // ʹ�ÿ����������ֺͲ������������������ؿ�����������Ӧ MethodInfo ���͵Ķ���
        var actionMethod = actionLocator.LocateActionMethod(ctrName!, actionName!);
        if (actionMethod == null)
        {
            await next(context);
            return;
        }
        Type controllerType = actionMethod.DeclaringType!;
        object controllerInstance = sp.GetRequiredService(controllerType);
        var paraValues = BindingHelper.GetParameterValues(context, actionMethod);
        // ����й���������ִ�� Action ǰ��ִ�� Filter
        // �м���� Filter �������м���� ASP.NETCore ����ṩ�Ļ��������ܣ��� Filter �����м�����ڲ�����Ӧ����
        // Ҳ���м���ǰ��� Filter �ģ�Filter �����м�����ṩ�Ĺ���
        // �м�����Դ������е����󣬶� Filter ֻ�ܴ�����Կ������������м��������һ�����ײ㡢������ļ���������м�����޷����� MCV ������еĸ���
        // �м���� Filter ������ɺܶ����ƵĹ��ܣ���δ�����쳣�м�����͡�δ�����쳣 Filter���������������м�����͡��������� Filter��������
        // ��������ʹ���м�����������������ֻ��� MVC ������Ҫ����һЩ MVC ��ص���ʱ����ֻ��ѡ�� Filter ��
        foreach (var filter in ActionFilters.Filters)
        {
            filter.Execute();
        }
        var result = actionMethod.Invoke(controllerInstance, paraValues);
        // �޶�����ֵֻ������ͨ���ͣ��������� IActionResult ��
        var jsonStr = JsonSerializer.Serialize(result);
        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json; charset=utf-8";
        await context.Response.WriteAsync(jsonStr);
    }

}