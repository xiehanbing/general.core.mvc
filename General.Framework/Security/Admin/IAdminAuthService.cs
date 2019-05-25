using System;
using System.Collections.Generic;
using General.Entity.Category;

namespace General.Framework.Security.Admin
{
    public interface IAdminAuthService
    {
        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        void SignIn(string token, string name);

        void SingOut();
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        Entity.User.SysUser GetCurrentUser();

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        List<Category> GetMyCategories();
    }
}