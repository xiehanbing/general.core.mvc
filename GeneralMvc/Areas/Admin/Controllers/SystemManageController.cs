using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using General.Framework.Menu;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Function("系统管理",true,"menu-icon fa fa-desktop")]
    public class SystemManageController : AdminPermissionController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}