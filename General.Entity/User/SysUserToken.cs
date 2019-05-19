using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entity.User
{
    [Serializable]
    [Table("SysUserToken")]
    public class SysUserToken
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SysUserId { get; set; }
        public DateTime ExpireTime { get; set; }
        [ForeignKey("SysUserId")]
        public virtual SysUser SysUser { get; set; }
    }
}