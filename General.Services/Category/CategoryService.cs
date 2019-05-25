using System;
using System.Collections.Generic;
using System.Linq;
using General.Core.Data;
using General.Entity;
using Microsoft.Extensions.Caching.Memory;

namespace General.Services.Category
{
    public class CategoryService : ICategoryService
    {
        //private readonly GeneralDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        private IRepository<Entity.Category.Category> _caRepository;
        private readonly IRepository<Entity.Category.SysPermission> _syspermissionRepository;
        private const string MODEL_KEY = "menu_key";
        public CategoryService(IRepository<Entity.Category.Category> caRepository,
            IRepository<Entity.Category.SysPermission> syspermissionRepository,
            IMemoryCache memoryCache)
        {
            _caRepository = caRepository;
            _syspermissionRepository = syspermissionRepository;
            _memoryCache = memoryCache;
        }
        public List<Entity.Category.Category> GetAll()
        {
            if (_memoryCache.TryGetValue<List<Entity.Category.Category>>(MODEL_KEY,
                out List<Entity.Category.Category> list))
            {
                return list;
            }

            var data = _caRepository.Table.ToList();
            _memoryCache.Set(MODEL_KEY, data,DateTimeOffset.Now.AddHours(1));
            return data;
        }
        /// <summary>
        /// 保存菜单到数据库
        /// </summary>
        /// <param name="list"></param>
        public void InitCategory(List<Entity.Category.Category> list)
        {
            var oldList = _caRepository.Table.ToList();
            //需要清除
            oldList.ForEach(del =>
            {
                var item = list.FirstOrDefault(o => o.SysResource == del.SysResource);
                if (item != null)
                {
                    var permissionList = del.SysPermissions.ToList();
                    permissionList.ForEach(delperm => { _syspermissionRepository.Entities.Remove(delperm); });
                    //_caRepository.Entities.Remove(del);
                }
            });

            list.ForEach(entity =>
            {
                var item = oldList.FirstOrDefault(o => o.SysResource == entity.SysResource);
                if (item == null)
                {
                    _caRepository.Entities.Add(entity);
                }
                else
                {
                    item.Action = entity.Action;
                    item.Controller = entity.Controller;
                    item.CssClass = entity.CssClass;
                    item.FatherResource = entity.FatherResource;
                    item.FatherId = entity.FatherId;
                    item.SysResource = entity.SysResource;
                    item.ResourceId = entity.ResourceId;
                    item.Sort = entity.Sort;
                    item.Name = entity.Name;
                    item.RouteName = entity.RouteName;
                }
            });
            if (_caRepository.DbContext.ChangeTracker.HasChanges())
            {
                _caRepository.DbContext.SaveChanges();
            }
        }

        public List<Entity.Category.Category> GetByUser(string userId)
        {
            return GetAll();
            //_memoryCache.TryGetValue<List<Entity.Category.Category>>("menu");
        }
    }
}