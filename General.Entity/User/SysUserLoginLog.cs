using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entity.User
{
    [Table("SysUserLoginLog")]
    public class SysUserLoginLog
    {
        [Key]
        public Guid  Id { get; set; }
        public Guid UserId { get; set; }
        public string  IpAddress { get; set; }
        public string Message  { get; set; }
        public DateTime LoginTime { get; set; }
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}