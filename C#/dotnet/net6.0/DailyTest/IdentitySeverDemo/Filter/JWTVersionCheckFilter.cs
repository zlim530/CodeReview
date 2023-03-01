using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace IdentitySeverDemo.Filter;

public class JWTVersionCheckFilter : IAsyncActionFilter
{
    private readonly UserManager<MyUser> userManager;

    public JWTVersionCheckFilter(UserManager<MyUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 不需要 JWToken 验证的方法不进行 JWTVersion 的 check
        if (context.HttpContext.Request.Headers.Authorization.Any() == false)
        {
            await next();
            return;
        }
        var jwtFromClaims = context.HttpContext.User.FindFirst("JWTVersion");
        if (jwtFromClaims == null)
        {
            context.Result = new ObjectResult("无效的 Payload 信息!") { StatusCode = 400};
            return;
        }
        var claimUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var user = await userManager.FindByIdAsync(claimUserId.Value);
        if (user == null)
        {
            context.Result = new ObjectResult("用户错误！") { StatusCode = 400 };
            return;
        }
        // 优化项：不用每次都查数据库，否则性能太低，可以使用缓存优化
        long claimJWTVer = Convert.ToInt64(jwtFromClaims.Value);
        if (user.JWTVersion > claimJWTVer)
        {
            context.Result = new ObjectResult("用户的 JWT 已过时！") { StatusCode = 400 };
            return;
        }
        await next();
    }
}