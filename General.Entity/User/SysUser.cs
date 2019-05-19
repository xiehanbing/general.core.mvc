using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace General.Entity.User
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    [Table("SysUser")]
    public class SysUser
    {
        public SysUser()
        {
            SysUserToken=new HashSet<SysUserToken>();
            SysUserLoginLogs=new HashSet<SysUserLoginLog>();
        }
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
        [Column("Sex")]
        public string  Sex { get; set; }
        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int  LoginFailedNum { get; set; }
        /// <summary>
        /// 用户的guid
        /// </summary>
        [Key]
        public Guid UserGuid { get; set; }

        /// <summary>
        /// 登录锁
        /// </summary>
        public bool LoginLock { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 允许登录的时间
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime? AllowLoginTime { get; set; }

        /// <summary>
        /// 是否被冻结
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 是否被删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 用户token
        /// </summary>
        public virtual ICollection<SysUserToken>  SysUserToken { get; set; }
        /// <summary>
        /// 用户登录日志
        /// </summary>
        public  virtual  ICollection<SysUserLoginLog> SysUserLoginLogs { get; set; }
    }
}