using System;
using System.Collections.Generic;
using System.Linq;
using General.Core.Data;
using Microsoft.Extensions.Caching.Memory;

namespace General.Services.SysPermission
{
    public class SysPermissionService : ISysPermissionService
    {
        private readonly IMemoryCache _memoryCache;
        private const string MODEL_KEY = "General.services.syspermission";
        private readonly IRepository<Entity.Category.SysPermission> _sysPermissionRepository;

        public SysPermissionService(IMemoryCache memoryCache,
            IRepository<Entity.Category.SysPermission> sysPermissionRepository)
        {
            _memoryCache = memoryCache;
            _sysPermissionRepository = sysPermissionRepository;
        }

        public List<Entity.Category.SysPermission> GetAll()
        {
            _memoryCache.TryGetValue<List<Entity.Category.SysPermission>>(MODEL_KEY,
                out List<Entity.Category.SysPermission> list);
            if (list != null) return list;
            var data = _sysPermissionRepository.Table.ToList();
            _memoryCache.Set(MODEL_KEY, data, DateTimeOffset.Now.AddHours(1));
            return data;
        }
    }
}