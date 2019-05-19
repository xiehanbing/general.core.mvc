using Microsoft.AspNetCore.Mvc;

namespace General.Framework.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            AjaxData=new AjaxResult();
        }
        /// <summary>
        /// 返回结果
        /// </summary>
        public AjaxResult AjaxData { get; }
    }
}