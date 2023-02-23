using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPIDemo.Controllers;

public class MyActionFilterTest2 : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("MyActionFilterTest2 �Զ��� ActionFilter ����");
        ActionExecutedContext result = await next();
        if (result.Exception != null)
        {
            Console.WriteLine("MyActionFilterTest2 �������쳣");
        }
        else
        {
            Console.WriteLine("MyActionFilterTest2 ִ�гɹ�");
        }
    }
}