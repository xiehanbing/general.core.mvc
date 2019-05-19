using System;

namespace General.Services.User
{
    public interface ISysUserService
    {
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="r">随机数</param>
        /// <returns></returns>
        (bool success, string message, string token, Entity.User.SysUser user) ValidateUser(string account, string password, string r);

        /// <summary>
        /// 根据账号获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Entity.User.SysUser GetByAccount(string account);

        bool Add(Entity.User.SysUser user);

        /// <summary>
        /// 根据当前登录的用户的token 获取用户信息 并缓存
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Entity.User.SysUser GetLogged(string token);
    }
}