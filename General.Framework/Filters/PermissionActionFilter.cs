using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Framework.Filters
{
    /// <summary>
    /// 权限过滤
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple = true,Inherited = true)]
    public class PermissionActionFilter:Attribute,IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}