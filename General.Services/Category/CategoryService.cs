using System.Collections.Generic;
using System.Linq;
using General.Core.Data;
using General.Entity;

namespace General.Services.Category
{
    public class CategoryService : ICategoryService
    {
        //private readonly GeneralDbContext _dbContext;
        private IRepository<Entity.Category.Category> _caRepository;
        private readonly IRepository<Entity.Category.SysPermission> _syspermissionRepository;
        public CategoryService(IRepository<Entity.Category.Category> caRepository, IRepository<Entity.Category.SysPermission> syspermissionRepository)
        {
            _caRepository = caRepository;
            _syspermissionRepository = syspermissionRepository;
        }
        public List<Entity.Category.Category> GetAll()
        {
            return _caRepository.Table.ToList();
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
                    _caRepository.Entities.Remove(del);
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
    }
}