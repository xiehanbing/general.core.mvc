using System;
using System.Security.Principal;

namespace General.Entity.User
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 个人账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string  Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string  MobilePhone { get; set; }
        /// <summary>
        /// 密码盐
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string  Sex { get; set; }
        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int  LoginFailedNum { get; set; }
        /// <summary>
        /// 用户的guid
        /// </summary>
        public Guid UserGuid { get; set; }
    }
}