using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using General.Core;
using General.Entity;
using General.Framework.Controllers;
using General.Services.Category;
using Microsoft.AspNetCore.Mvc;
using GeneralMvc.Models;

namespace General.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        private ICategoryService _categoryService;
        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            //_categoryService = EngineContext.CurrentEngin.Resolve<ICategoryService>();
            //var list = _categoryService.GetAll();
            //var list = _categoryService.GetAll();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
