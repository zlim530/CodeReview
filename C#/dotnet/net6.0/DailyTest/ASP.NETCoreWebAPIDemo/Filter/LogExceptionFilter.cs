using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPIDemo;

public class LogExceptionFilter : IAsyncExceptionFilter
{
    /// <summary>
    /// 当异步方法体只有一条语句并且返回结果为 Task 类型时，调用异步方法时可以不用写 await, 对应在方法声明上也不用加上 async
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnExceptionAsync(ExceptionContext context)
    {
        return File.AppendAllTextAsync("d:/error.log", context.Exception.ToString());
    }
}