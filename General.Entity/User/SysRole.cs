using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using General.Entity.Category;

namespace General.Entity.User
{
    [Table("SysRole")]
    public class SysRole
    {
        public SysRole()
        {
            SysPermissions=new HashSet<SysPermission>();
            SysUserRoles = new HashSet<SysUserRole>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Descrption { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid Creator { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public Guid? Modifier { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }=DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }
        /// <summary>
        /// 外键创建者
        /// </summary>
        [ForeignKey("Creator")]
        public virtual SysUser SysCreatorUser { get; set; }
        /// <summary>
        /// 外键 修改者
        /// </summary>
        [ForeignKey("Modifier")]
        public virtual SysUser SysModifiedUser { get; set; }
        /// <summary>
        /// 权限配置
        /// </summary>
        public virtual  ICollection<SysPermission> SysPermissions { get; set; }
        /// <summary>
        /// 用户-角色
        /// </summary>
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
}