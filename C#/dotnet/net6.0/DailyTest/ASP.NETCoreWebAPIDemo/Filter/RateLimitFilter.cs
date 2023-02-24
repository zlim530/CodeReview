using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NETCoreWebAPIDemo.Filter;

public class RateLimitFilter : IAsyncActionFilter
{
    private readonly IMemoryCache memoryCache;

    public RateLimitFilter(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
        var cacheKey = $"lastvisittick_{ip}";
        long? lastVisit = memoryCache.Get<long?>(cacheKey);
        if (lastVisit == null || Environment.TickCount64 - lastVisit > 1000) 
        {
            memoryCache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10));
            // ���ⳤ�ڲ����ʵ��û�ռ�ݳ����ڴ滺����Դ��������û�����Թ���ʱ��Ϊ10s
            return next();
        }
        else
        {
            var result = new ObjectResult("��ǰ�û����ʷ���������Ƶ�������Ե�1�������") { StatusCode = 429};
            context.Result = result;
            return Task.CompletedTask;
        }
    }
}