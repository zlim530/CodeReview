using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace ASP.NETCoreWebAPIDemo.Filter;

public class TransactionScopeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // context.ActionDescriptor ���ǵ�ǰ��ִ�е� Action ������������Ϣ
        // context.ActionArguments ���ǵ�ǰ��ִ�е� Action �����Ĳ�����Ϣ
        // controllerActionDescriptor.MethodInfo ��ǰִ�е� Action ����
        #region ��ʼ�ֲ�ʵ�ְ汾
        //ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        //bool isTransactionFilter = false;// �Ƿ�����������
        //if (controllerActionDescriptor != null) // ���Ϊ null ���ʾ����һ�� MVC �� Action ��������Ϊ ASP .NET Core ������ blazor �ȼ���
        //{
        //    var hasNotTransationAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(NotTranscationAttibute), false).Any();
        //    isTransactionFilter = !hasNotTransationAttribute;
        //}
        //if (isTransactionFilter) 
        //{
        //    using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        var r = await next();
        //        if (r.Exception == null) 
        //        {
        //            tx.Complete();
        //        }
        //    }
        //}
        //else
        //{
        //    await next();
        //}
        #endregion

        bool hasNotTransactionlAttribute = false;
        if (context.ActionDescriptor is ControllerActionDescriptor)
        {
            var actionDesc = (ControllerActionDescriptor)context.ActionDescriptor;
            hasNotTransactionlAttribute = actionDesc.MethodInfo.IsDefined(typeof(NotTranscationAttibute));
        }
        if (hasNotTransactionlAttribute) 
        {
            await next();
            return;
        }
        using var txScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await next();
        if (result.Exception == null)
        {
            txScope.Complete();
        }

    }
}