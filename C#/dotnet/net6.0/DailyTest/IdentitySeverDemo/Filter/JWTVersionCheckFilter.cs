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
        // ����Ҫ JWToken ��֤�ķ��������� JWTVersion �� check
        if (context.HttpContext.Request.Headers.Authorization.Any() == false)
        {
            await next();
            return;
        }
        var jwtFromClaims = context.HttpContext.User.FindFirst("JWTVersion");
        if (jwtFromClaims == null)
        {
            context.Result = new ObjectResult("��Ч�� Payload ��Ϣ!") { StatusCode = 400};
            return;
        }
        var claimUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var user = await userManager.FindByIdAsync(claimUserId.Value);
        if (user == null)
        {
            context.Result = new ObjectResult("�û�����") { StatusCode = 400 };
            return;
        }
        // �Ż������ÿ�ζ������ݿ⣬��������̫�ͣ�����ʹ�û����Ż�
        long claimJWTVer = Convert.ToInt64(jwtFromClaims.Value);
        if (user.JWTVersion > claimJWTVer)
        {
            context.Result = new ObjectResult("�û��� JWT �ѹ�ʱ��") { StatusCode = 400 };
            return;
        }
        await next();
    }
}