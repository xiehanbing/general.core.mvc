using System;
using System.Collections.Generic;
using System.Linq;
using General.Core.Data;
using General.Entity.User;
using Microsoft.Extensions.Caching.Memory;

namespace General.Services.SysUserRole
{
    public class SysUserRoleService : ISysUserRoleService
    {
        private readonly IMemoryCache _memoryCache;
        private const string MODEL_KEY = "General.services.userRole";
        private readonly IRepository<Entity.User.SysUserRole> _sysUserRoleRepository;

        public SysUserRoleService(IMemoryCache memoryCache, IRepository<Entity.User.SysUserRole> sysUserRoleRepository)
        {
            _memoryCache = memoryCache;
            _sysUserRoleRepository = sysUserRoleRepository;
        }

        public List<Entity.User.SysUserRole> GetAll()
        {
            _memoryCache.TryGetValue<List<Entity.User.SysUserRole>>(MODEL_KEY, out List<Entity.User.SysUserRole> list);

            if (list != null) return list;

            var data = _sysUserRoleRepository.Table.Where(o => !o.IsDeleted).ToList();
            _memoryCache.Set(MODEL_KEY, data, DateTimeOffset.Now.AddHours(1));
            return data;
        }
    }
}