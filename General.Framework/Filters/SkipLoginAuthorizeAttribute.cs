using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Framework.Filters
{
    /// <summary>
    /// 跳过属性检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SkipLoginAuthorizeAttribute : Attribute, IFilterMetadata
    {
        
    }
}