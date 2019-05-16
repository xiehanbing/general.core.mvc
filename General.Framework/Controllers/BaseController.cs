using Microsoft.AspNetCore.Mvc;

namespace General.Framework.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public AjaxResult AjaxData => new AjaxResult();
    }
}