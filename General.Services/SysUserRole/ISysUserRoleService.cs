using System.Collections.Generic;

namespace General.Services.SysUserRole
{
    public interface ISysUserRoleService
    {
        List<Entity.User.SysUserRole> GetAll();
    }
}