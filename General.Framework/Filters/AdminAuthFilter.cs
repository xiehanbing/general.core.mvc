using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Framework.Filters
{
    /// <summary>
    /// 登录状态过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminAuthFilter:Attribute,IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //throw new System.NotImplementedException();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new System.NotImplementedException();
        }
    }
}