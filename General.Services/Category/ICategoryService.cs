using System.Collections.Generic;

namespace General.Services.Category
{
    public interface ICategoryService
    {
        List<Entity.Category.Category> GetAll();

        void InitCategory(List<Entity.Category.Category> list);

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        List<Entity.Category.Category> GetByUser(string userId);
    }
}