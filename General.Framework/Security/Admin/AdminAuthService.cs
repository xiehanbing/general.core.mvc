using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using General.Entity.Category;
using General.Entity.User;
using General.Services.Category;
using General.Services.SysPermission;
using General.Services.SysUserRole;
using General.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace General.Framework.Security.Admin
{
    public class AdminAuthService : IAdminAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISysUserService _sysUserService;
        private readonly ICategoryService _categoryService;
        private readonly ISysUserRoleService _sysUserRoleService;
        private readonly ISysPermissionService _sysPermissionService;
        public AdminAuthService(IHttpContextAccessor httpContextAccessor, ISysUserService sysUserService,
            ICategoryService categoryService, ISysUserRoleService sysUserRoleService,
            ISysPermissionService sysPermissionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _sysUserService = sysUserService;
            _categoryService = categoryService;
            _sysUserRoleService = sysUserRoleService;
            _sysPermissionService = sysPermissionService;
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

        public List<Category> GetMyCategories()
        {
            var cateList = _categoryService.GetAll();
            var user = GetCurrentUser();
            if (user == null) return null;
            if (user.IsAdmin) return cateList;
            var roleList = _sysUserRoleService.GetAll();
            if (roleList == null || roleList?.Count <= 0) return null;
            var roleIds = roleList.Where(o => o.UserId == user.UserGuid).Select(x => x.RoleId).Distinct().ToList();

            var permissionList = _sysPermissionService.GetAll();
            if (permissionList == null || permissionList.Count <= 0) return null;
            var list = permissionList.Where(o => roleIds.Contains(o.RoleId)).Select(x => x.CategoryId).Distinct().ToList();
            if (list.Count <= 0) return null;
            var result = cateList.Where(o => list.Contains(o.Id)).Distinct().ToList();
            return result;
        }

        public void SingOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAdminAuthInfo.AdminAuthCookieScheme);
        }
    }
}