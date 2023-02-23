using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace ASP.NETCoreWebAPIDemo.Filter;

public class TransactionScopeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // context.ActionDescriptor 中是当前被执行的 Action 方法的描述信息
        // context.ActionArguments 中是当前被执行的 Action 方法的参数信息
        ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        bool isTransactionFilter = false;// 是否进行事务控制
        if (controllerActionDescriptor != null) // 如果为 null 则表示不是一个 MVC 的 Action 方法，因为 ASP .NET Core 还包含 blazor 等技术
        {
            // controllerActionDescriptor.MethodInfo 当前执行的 Action 方法
            var hasNotTransationAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(NotTranscationAttibute), false).Any();
            isTransactionFilter = !hasNotTransationAttribute;
        }
        if (isTransactionFilter) 
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var r = await next();
                if (r.Exception == null) 
                {
                    tx.Complete();
                }
            }
        }
        else
        {
            await next();
        }
    }
}