using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPIDemo.Controllers;

public class MyActionFilterTest1 : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("MyActionFilterTest1 自定义 ActionFilter 方法");
        ActionExecutedContext result = await next();
        if (result.Exception != null)
        {
            Console.WriteLine("MyActionFilterTest1 发生了异常");
        }
        else
        {
            Console.WriteLine("MyActionFilterTest1 执行成功");
        }
    }
}