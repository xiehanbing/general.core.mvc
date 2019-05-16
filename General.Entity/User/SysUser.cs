using System;

namespace General.Entity.User
{
    public class SysUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}