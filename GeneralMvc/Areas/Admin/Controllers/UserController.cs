using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using General.Framework.Menu;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Route("admin/user")]
    public class UserController : AdminPermissionController
    {
        [Function("系统用户",true,"menu-icon fa fa-caret-right",FatherResource = "General.Mvc.Areas.Admin.Controllers.SystemManageController", Sort = 0)]
        public IActionResult Index()
        {
            return View();
        }
    }
}