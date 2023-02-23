using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPIDemo.Controllers;

public class MyActionFilterTest1 : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("MyActionFilterTest1 �Զ��� ActionFilter ����");
        ActionExecutedContext result = await next();
        if (result.Exception != null)
        {
            Console.WriteLine("MyActionFilterTest1 �������쳣");
        }
        else
        {
            Console.WriteLine("MyActionFilterTest1 ִ�гɹ�");
        }
    }
}