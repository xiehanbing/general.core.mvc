using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entity.User
{
    /// <summary>
    /// 角色-用户对应表
    /// </summary>
    [Table("SysUserRole")]
    public class SysUserRole
    {
        /// <summary>
        /// 自增
        /// </summary>
        [Key]
        public int  Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 外键userid
        /// </summary>
        [ForeignKey("UserId")]
        public virtual  SysUser SysUser { get; set; }
        /// <summary>
        /// 外键role
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual  SysRole  SysRole { get; set; }
    }
}