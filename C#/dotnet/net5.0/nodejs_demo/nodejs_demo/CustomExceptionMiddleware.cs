using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace nodejs_demo
{
    /// <summary>
    /// 2.创建自定义的中间件来实现我们的自定义异常处理：
    /// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(
            RequestDelegate next,
            ILogger<CustomExceptionMiddleware> logger
            )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unhandled exception ... ",ex);
                await HandleExceptionAsync(httpContext, ex);
                throw;
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline
    /// </summary>
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCutomeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
