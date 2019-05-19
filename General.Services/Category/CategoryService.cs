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
            oldList.ForEach(del =>
            {
                var item = list.FirstOrDefault(o => o.SysResource == del.SysResource);
                if (item != null)
                {
                    var permissionList=del.sy
                }
            });
        }
    }
}