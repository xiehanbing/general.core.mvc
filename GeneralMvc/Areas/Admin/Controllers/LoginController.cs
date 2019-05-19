using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core.Librs;
using General.Entity.User;
using General.Framework.Controllers.Admin;
using General.Framework.Filters;
using General.Framework.Security.Admin;
using General.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理登录器
    /// </summary>
    [Route("admin"),SkipLoginAuthorize]
    public class LoginController : AdminAreaController
    {
        private const string R_KEY = "R_KEY";
        private ISysUserService _sysUserService;
        private IAdminAuthService _authenticationService;

        public LoginController(ISysUserService sysUserService, IAdminAuthService authenticationService)
        {
            _sysUserService = sysUserService;
            _authenticationService = authenticationService;
        }
        [Route("login", Name = "adminlogin")]
        public IActionResult Index()
        {
            string s = EncryptorHelper.GetMd5(Guid.NewGuid().ToString());

            HttpContext.Session.SetString(R_KEY, s);
            //HttpContext.Response.Cookies.Append(R_KEY,s);
            LoginModel loginModel=new LoginModel()
            {
                Range = s
            };
            return View(loginModel);
        }

        [Route("register")]
        public IActionResult Regiter()
        {
            var salt = Guid.NewGuid().ToString();
            var password = EncryptorHelper.GetMd5("123456" + salt);
            var data = _sysUserService.Add(new SysUser()
            {

                Account = "xiehanbing",
                Password = password,
                UserGuid = Guid.NewGuid(),
                LastLoginTime = DateTime.Now,
                Name = "谢汉冰",
                Salt = salt,
                LoginFailedNum = 0,
                LoginLock = false,
            });
            if (data)
            {
                AjaxData.Status = true;
                return Json(AjaxData);
            }
            return Json(AjaxData);
        }

        [Route("login"), HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                AjaxData.Message = "请输入账号和密码";
                return Json(AjaxData);
            }
            var r = HttpContext.Session.GetString(R_KEY)??"";
            //var r = model.Range ?? "";
            //如果要 获取session 请将CookiePolicyOptions CheckConsentNeeded 设置为false
           
            var data = _sysUserService.ValidateUser(model.Account, model.Password, r);
            AjaxData.Status = data.success;
            if (!data.success)
            {
                AjaxData.Message = data.Item2;
                return Json(AjaxData);
            }
            //保存登录状态 ClaimsIdentity ClaimsPrincipal
            _authenticationService.SignIn(data.token, data.user.Name);


            return Json(AjaxData);
        }
        /// <summary>
        /// 获取密码盐
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Route("getSalt",Name = "getSalt")]
        public IActionResult GetSalt(string account)
        {
            var user = _sysUserService.GetByAccount(account);
            return Content(user?.Salt);
        }
    }
}