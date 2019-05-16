﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    public class MainController : PublicAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}