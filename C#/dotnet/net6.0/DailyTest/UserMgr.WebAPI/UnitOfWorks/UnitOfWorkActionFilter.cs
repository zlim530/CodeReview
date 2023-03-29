using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UserMgr.WebAPI.UnitOfWorks
{
    /// <summary>
    /// 工作单元是由应用服务层来确定，其他层不应该调用SaveChangesAsync方法保存对数据的修改
    /// 可以开发一个在控制器的方法调用结束后自动调用SaveChangesAsync的Filter：UnitOfWorkAttribute、UnitOfWorkFilter
    /// </summary>
    public class UnitOfWorkActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDesc == null) 
            {
                return;
            }
            var unitOfWorkAttr = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            if (unitOfWorkAttr == null)
            {
                return;
            }
            foreach (var dbCtxType in unitOfWorkAttr.DbContextTypes)
            {
                // 向 DI 容器要 DbContext 的实例
                var dbCtx = context.HttpContext.RequestServices.GetService(dbCtxType) as DbContext;
                if (dbCtx != null)
                {
                    var result = await next();
                    if (result.Exception == null)// 只有 Action 方法执行成功，才自动调用 SaveChangeAsync 方法
                    {
                        await dbCtx.SaveChangesAsync();
                    }
                    Console.WriteLine("Done SaveChangesAsync(). ");
                }
            }
        }
    }
}
