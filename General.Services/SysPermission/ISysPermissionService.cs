using System.Collections.Generic;

namespace General.Services.SysPermission
{
    public interface ISysPermissionService
    {
        List<Entity.Category.SysPermission> GetAll();
    }
}