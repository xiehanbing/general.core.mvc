using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entity.Category
{
    [Table("SysRole")]
    public class SysRole
    {
        public SysRole()
        {
            SysPermissions=new HashSet<SysPermission>();
        }
        /// <summary>
        /// 自增id
        /// </summary>
        [Key]
        public int  Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string  Name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string  Descrption { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 外键
        /// </summary>
        public virtual ICollection<SysPermission> SysPermissions { get; set; }
    }
}