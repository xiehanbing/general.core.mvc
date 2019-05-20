using System;
using System.Threading.Tasks;
using General.Framework;
using General.Framework.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace General.Mvc.MyMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next) { _next = next; }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            string msg = exception.Message;
            if (context.Request.IsAjaxRequest())
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                if (exception is MyException myException)
                {
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new AjaxResult()
                    {
                        Status = false,
                        Message = myException.Message,
                        ErorCode = myException.Code
                    }));
                }
                else if (exception.InnerException != null && exception.InnerException is MyException myInnerException)
                {
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new AjaxResult()
                    {
                        Status = false,
                        Message = myInnerException.Message,
                        ErorCode = myInnerException.Code
                    }));
                }
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new AjaxResult()
            {
                Status = false,
                Message = "系统异常",
                ErorCode = 0
            }));
            //throw exception;
        }

    }
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}