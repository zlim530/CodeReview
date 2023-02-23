using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPIDemo;

public class LogExceptionFilter : IAsyncExceptionFilter
{
    /// <summary>
    /// ���첽������ֻ��һ����䲢�ҷ��ؽ��Ϊ Task ����ʱ�������첽����ʱ���Բ���д await, ��Ӧ�ڷ���������Ҳ���ü��� async
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnExceptionAsync(ExceptionContext context)
    {
        return File.AppendAllTextAsync("d:/error.log", context.Exception.ToString());
    }
}