using General.Entity.Category;
using General.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace General.Entity
{
    public class GeneralDbContext : DbContext
    {
        public GeneralDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category.Category> Categories { get; set; }

        public DbSet<User.SysUser> Users { get; set; }

        public DbSet<SysUserToken> SysUserTokens { get; set; }

        public DbSet<SysUserLoginLog> SysUserLoginLogs { get; set; }

        public DbSet<SysPermission> SysPermissions { get; set; }

        public DbSet<SysRole> SysRoles { get; set; }
    }
}