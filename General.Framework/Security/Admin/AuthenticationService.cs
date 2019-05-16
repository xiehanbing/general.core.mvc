using System;
using General.Entity.User;

namespace General.Framework.Security.Admin
{
    public class AuthenticationService:IAuthenticationService
    {
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public SysUser GetCurrentUser()
        {
            return new SysUser()
            {
                Id=Guid.NewGuid(),
                LoginName = "xiehanbing",
                Name = "谢汉冰",
                Password = "123456"
            };
        }
    }
}