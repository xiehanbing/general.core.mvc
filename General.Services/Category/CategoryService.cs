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
        public CategoryService(IRepository<Entity.Category.Category> caRepository)
        {
            _caRepository = caRepository;
        }
        public List<Entity.Category.Category> GetAll()
        {
            return _caRepository.Table.ToList();
        }

        public void InitCategory(List<Entity.Category.Category> list)
        {
            var oldList = _caRepository.Table.ToList();
            //需要清除
            oldList.ForEach(del =>
            {
                var item = list.FirstOrDefault(o => o.SysResource == del.SysResource);
                if (item != null)
                {
                    //var permissionList=del.sy
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