using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    public class MainController : PublicAdminController
    {
        private IAdminAuthService _adminAuthService;

        public MainController(IAdminAuthService adminAuthService)
        {
            _adminAuthService = adminAuthService;
        }
        public IActionResult Index()
        {
            var user = _adminAuthService.GetCurrentUser();
            return View();
        }


        public IActionResult SingOut()
        {
            _adminAuthService.SingOut();
            return RedirectToRoute("adminlogin");
        }
    }
}