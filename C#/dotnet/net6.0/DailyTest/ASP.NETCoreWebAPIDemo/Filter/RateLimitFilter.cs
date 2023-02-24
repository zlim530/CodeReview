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
            // 避免长期不访问的用户占据程序内存缓存资源，因此设置缓存绝对过期时间为10s
            return next();
        }
        else
        {
            var result = new ObjectResult("当前用户访问服务器过于频繁，请稍等1秒后再试") { StatusCode = 429};
            context.Result = result;
            return Task.CompletedTask;
        }
    }
}