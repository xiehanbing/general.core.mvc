using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entity.Category
{
    /// <summary>
    /// 角色菜单配置表
    /// </summary>
    [Table("SysPermission")]
    [Serializable]
    public class SysPermission
    {
        /// <summary>
        /// 自增id
        /// </summary>
        [Key]
        public int  Id { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public int  CategoryId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int  RoleId { get; set; }
        /// <summary>
        /// 外键 菜单id
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual SysRole SysRole { get; set; }
    }
}