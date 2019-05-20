using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entity.User
{
    /// <summary>
    /// 登录记录
    /// </summary>
    [Table("SysUserLoginLog")]
    public class SysUserLoginLog
    {
        /// <summary>
        /// 自增id
        /// </summary>
        [Key]
        public Guid  Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string  IpAddress { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message  { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 外键
        /// </summary>
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}