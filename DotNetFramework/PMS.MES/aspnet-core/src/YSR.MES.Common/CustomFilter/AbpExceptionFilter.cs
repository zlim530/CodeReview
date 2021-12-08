using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.AspNetCore.Mvc.Results;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Logging;
using Abp.Runtime.Validation;
using Abp.UI;
using Abp.Web.Configuration;
using Abp.Web.Models;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Reflection;

namespace YSR.MES.Common.CustomFilter
{
    public class AbpExceptionFilter : IExceptionFilter, ITransientDependency
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// 事件总线
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 错误信息构建器
        /// </summary>
        private readonly IErrorInfoBuilder _errorInfoBuilder;
        // AspNetCore 相关的配置信息
        private readonly IAbpAspNetCoreConfiguration _configuration;
        private readonly IAbpWebCommonModuleConfiguration _abpWebCommonModuleConfiguration;

        public AbpExceptionFilter(
            IErrorInfoBuilder errorInfoBuilder,
            IAbpAspNetCoreConfiguration configuration,
            IAbpWebCommonModuleConfiguration abpWebCommonModuleConfiguration)
        {
            _errorInfoBuilder = errorInfoBuilder;
            _configuration = configuration;
            _abpWebCommonModuleConfiguration = abpWebCommonModuleConfiguration;

            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
        }

        // 异常触发时会调用此方法
        public void OnException(ExceptionContext context)
        {
            // 判断是否由控制器触发，如果不是则不做任何处理
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            //var wrapResultAttribute = context.ActionDescriptor.GetMethodInfo().GetCustomAttributes(true).OfType<WrapResultAttribute>().FirstOrDefault()
            //       ?? context.ActionDescriptor.GetMethodInfo().ReflectedType?.GetTypeInfo().GetCustomAttributes(true).OfType<WrapResultAttribute>().FirstOrDefault()
            //       ?? new WrapResultAttribute();

            var wrapResultAttribute = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                context.ActionDescriptor.GetMethodInfo(),
                _configuration.DefaultWrapResultAttribute
            );

            // 如果方法上面的包装特性要求记录日志，则记录日志
            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            HandleAndWrapException(context, wrapResultAttribute);
        }

        // 处理并包装异常
        protected virtual void HandleAndWrapException(ExceptionContext context, WrapResultAttribute wrapResultAttribute)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            //var displayUrl = context.HttpContext.Request.GetDisplayUrl();
            //if (_abpWebCommonModuleConfiguration.WrapResultFilters.HasFilterForWrapOnError(displayUrl,
            //    out var wrapOnError))
            //{
            //    context.HttpContext.Response.StatusCode = GetStatusCode(context, wrapOnError);

            //    if (!wrapOnError)
            //    {
            //        return;
            //    }

            //    HandleError(context);
            //    return;
            //}

            //context.HttpContext.Response.StatusCode = GetStatusCode(context, wrapResultAttribute.WrapOnError);

            //if (!wrapResultAttribute.WrapOnError)
            //{
            //    return;
            //}

            //HandleError(context);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            var errorInfo = _errorInfoBuilder.BuildForException(context.Exception);
            errorInfo.Message = context.Exception.Message;
            errorInfo.Code = GetStatusCode(context, errorInfo);
            context.Result = new ObjectResult(
                new AjaxResponse(
                    errorInfo,
                    context.Exception is AbpAuthorizationException
                )
            );

            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));

            context.Exception = null; //Handled!
        }

        private void HandleError(ExceptionContext context)
        {
            context.Result = new ObjectResult(
                new AjaxResponse(
                    _errorInfoBuilder.BuildForException(context.Exception),
                    context.Exception is AbpAuthorizationException
                )
            );

            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));

            context.Exception = null; // Handled!
        }

        // 根据不同的异常类型返回不同的 HTTP 错误码
        protected virtual int GetStatusCode(ExceptionContext context, /*bool wrapOnError*/ErrorInfo errorInfo)
        {
            if (context.Exception is AbpAuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (context.Exception is AbpValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            //if (wrapOnError)
            //{
            //    return (int)HttpStatusCode.InternalServerError;
            //}

            if (context.Exception is UserFriendlyException)
            {
                return errorInfo.Code;
            }

            return context.HttpContext.Response.StatusCode;
        }
    }
}
