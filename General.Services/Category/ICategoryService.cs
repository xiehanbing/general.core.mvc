using System.Collections.Generic;

namespace General.Services.Category
{
    public interface ICategoryService
    {
        List<Entity.Category.Category> GetAll();

        void InitCategory(List<Entity.Category.Category> list);
    }
}