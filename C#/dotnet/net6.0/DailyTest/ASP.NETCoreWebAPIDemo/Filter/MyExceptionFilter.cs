using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPIDemo;

public class MyExceptionFilter : IAsyncExceptionFilter
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public MyExceptionFilter(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        // ExceptionContext 对象的三个重要属性：
        // context.Exception 代表异常信息对象
        // 如果给 context.ExceptionHandled 赋值为 true,则其他 ExceptionFilter 将不会执行
        // context.Result 的值会输出给客户端
        string msg;
        // 在开发环境下打印异常的详细信息，但在生产环境中仅显示服务器端有异常发生，不打印异常堆栈信息（安全考虑）
        if (webHostEnvironment.IsDevelopment())
        {
            msg = context.Exception.ToString();
        }
        else 
        {
            msg = "服务器端发生未处理的异常";
        }
        var objectResult = new ObjectResult(new { code = 500, message = msg});
        context.Result = objectResult;
        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}