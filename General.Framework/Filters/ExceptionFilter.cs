using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Framework.Filters
{
    public class ExceptionFilter : IExceptionFilter, IFilterMetadata
    {
        public void OnException(ExceptionContext context)
        {
            var requstUrl = context.HttpContext.Request.Path;
            if (context.ExceptionHandled == false)
            {
                string msg = context.Exception.Message;
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    if (context.Exception is MyException myException)
                    {
                        context.Result = new JsonResult(new AjaxResult()
                        {
                            Status = false,
                            Message = myException.Message,
                            ErorCode = myException.Code,
                            
                        });
                    }
                    else if (context.Exception.InnerException != null && context.Exception is MyException myInnerException)
                    {
                        context.Result = new JsonResult(new AjaxResult()
                        {
                            Status = false,
                            Message = myInnerException.Message,
                            ErorCode = myInnerException.Code
                        });
                    }
                    else
                    {
                        context.Result = new JsonResult(new AjaxResult()
                        {
                            Status = false,
                            Message ="系统异常",
                            ErorCode =0
                        });
                    }

                }
                else
                {
                    RedirectResult redirectResult = new RedirectResult("~/Home/Error?message" + context.Exception.Message);
                    context.Result = redirectResult;
                }
            }
            context.ExceptionHandled = true;//异常已经处理了
        }
    }
}