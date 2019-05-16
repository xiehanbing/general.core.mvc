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
    }
}