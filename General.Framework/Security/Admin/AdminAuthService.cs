using System;
using System.Security.Claims;
using General.Entity.User;
using General.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace General.Framework.Security.Admin
{
    public class AdminAuthService : IAdminAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISysUserService _sysUserService;
        public AdminAuthService(IHttpContextAccessor httpContextAccessor, ISysUserService sysUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            _sysUserService = sysUserService;
        }
        /// <summary>
        /// 保存登录信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        public void SignIn(string token, string name)
        {
            ClaimsIdentity identity = new ClaimsIdentity("Forms");
            identity.AddClaim(new Claim(ClaimTypes.Sid, token));
            identity.AddClaim(new Claim(ClaimTypes.Name, name));
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            _httpContextAccessor.HttpContext.SignInAsync(CookieAdminAuthInfo.AdminAuthCookieScheme, principal);
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public SysUser GetCurrentUser()
        {
            var data = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAdminAuthInfo.AdminAuthCookieScheme)
                .Result;
            var token = data.Principal?.FindFirst(ClaimTypes.Sid)?.Value;
            if (token == null) return null;
            var result = _sysUserService.GetLogged(token);

            return result;

        }

        public void SingOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAdminAuthInfo.AdminAuthCookieScheme);
        }
    }
}