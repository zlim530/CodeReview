using Abp.AspNetCore.Mvc.Extensions;
using Abp.AspNetCore.Mvc.Results;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Web.Models;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace YSR.MES.Common.CustomFilter
{
    /// <summary>
    /// 权限验证过滤器
    /// </summary>
    public class AbpAuthorizationFilter : IAsyncAuthorizationFilter, ITransientDependency
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        public ILogger Logger { get; set; }

        // 权限验证类，这个才是真正针对权限进行验证的对象
        private readonly IAuthorizationHelper _authorizationHelper;
        // 异常包装器主要是用来封装没有授权时返回的错误信息
        private readonly IErrorInfoBuilder _errorInfoBuilder;
        // 事件总线处理器在这里用于触发一个未授权请求引发的事件，用户可以监听此事件来进行自己的处理
        private readonly IEventBus _eventBus;

        /// <summary>
        /// 构造函数依赖注入
        /// </summary>
        /// <param name="authorizationHelper"></param>
        /// <param name="errorInfoBuilder"></param>
        /// <param name="eventBus"></param>
        public AbpAuthorizationFilter(
            IAuthorizationHelper authorizationHelper,
            IErrorInfoBuilder errorInfoBuilder,
            IEventBus eventBus)
        {
            _authorizationHelper = authorizationHelper;
            _errorInfoBuilder = errorInfoBuilder;
            _eventBus = eventBus;
            Logger = NullLogger.Instance;
        }

        public virtual async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var endpoint = context?.HttpContext?.GetEndpoint();

            // 如果注入了 IAllowAnonymous 接口则允许所有匿名用户的请求
            // Allow Anonymous skips all authorization
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                return;
            }

            // 如果不是一个控制器方法则直接返回
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            //TODO: Avoid using try/catch, use conditional checking
            // 开始使用 IAuthorizationHelper 对象来进行权限校验
            try
            {
                await _authorizationHelper.AuthorizeAsync(
                    context.ActionDescriptor.GetMethodInfo(),
                    context.ActionDescriptor.GetMethodInfo().DeclaringType
                );
            }
            // 如果是未授权异常的处理逻辑
            catch (AbpAuthorizationException ex)
            {
                // 记录日志
                Logger.Warn(ex.ToString(), ex);

                // 触发异常事件
                await _eventBus.TriggerAsync(this, new AbpHandledExceptionData(ex));

                // 如果接口的返回类型为 ObjectResult，则采用 AjaxResponse 对象进行封装信息
                if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                {
                    //context.Result = new ObjectResult(new AjaxResponse(_errorInfoBuilder.BuildForException(ex), true))
                    //{
                    //    StatusCode = context.HttpContext.User.Identity.IsAuthenticated
                    //        ? (int)System.Net.HttpStatusCode.Forbidden
                    //        : (int)System.Net.HttpStatusCode.Unauthorized
                    //};
                    //获取错误信息
                    var errorInfo = _errorInfoBuilder.BuildForException(ex);
                    //code设置状态码数据
                    errorInfo.Code = (context.HttpContext.User.Identity.IsAuthenticated ? (int)System.Net.HttpStatusCode.Forbidden : (int)System.Net.HttpStatusCode.Unauthorized);
                    //返回结果
                    context.Result = new ObjectResult(new AjaxResponse(errorInfo, unAuthorizedRequest: true))
                    {
                        //默认状态
                        StatusCode = (int)System.Net.HttpStatusCode.OK
                    };
                }
                else
                {
                    context.Result = new ChallengeResult();
                }
            }
            // 其他异常则显示为服务器内部异常
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), ex);

                await _eventBus.TriggerAsync(this, new AbpHandledExceptionData(ex));

                if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                {
                    //context.Result = new ObjectResult(new AjaxResponse(_errorInfoBuilder.BuildForException(ex)))
                    //{
                    //    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
                    //};
                    //获取错误信息
                    var errorInfo = _errorInfoBuilder.BuildForException(ex);
                    errorInfo.Details = ex.Message;
                    errorInfo.Code = (int)System.Net.HttpStatusCode.InternalServerError;
                    context.Result = new ObjectResult(new AjaxResponse(errorInfo))
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.OK
                    };
                }
                else
                {
                    //TODO: How to return Error page?
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}
