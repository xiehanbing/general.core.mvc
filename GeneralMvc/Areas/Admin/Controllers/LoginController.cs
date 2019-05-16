using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理登录器
    /// </summary>
    [Route("admin/login")]
    public class LoginController : AdminAreaController
    {
        [Route("",Name = "adminlogin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}