using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using General.Entity.Category;
using General.Entity.User;
using General.Framework.Menu;
using General.Framework.Security.Admin;
using General.Services.Category;

namespace General.Framework.Infrastructure
{
    public class WorkContext:IWorkContext
    {
        private IAdminAuthService _authenticationService;
        private ICategoryService _categoryService;
        public WorkContext(IAdminAuthService authenticationService,ICategoryService categoryService)
        {
            _authenticationService = authenticationService;
            _categoryService = categoryService;


        }
        public SysUser CurrentUser
        {
            get { return _authenticationService.GetCurrentUser(); }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void InitRegister()
        {
            List<Entity.Category.Category> list = new List<Category>();
            FunctionManager.GetFunctionLists().ForEach(item =>
            {
                list.Add(new Category()
                {
                    Name = item.Name,
                    Action = item.Action,
                    Controller = item.Controller,
                    CssClass = item.CssClass,
                    FatherResource = item.FatherResource,
                    FatherId = item.FatherId,
                    SysResource = item.SysResource,
                    ResourceId = item.ResourceId,
                    RouteName = item.RouteName,
                    Sort = item.Sort,
                    IsMenu = item.IsMenu,
                    IsDisabled = item.IsDisabled
                });

            });

            _categoryService.InitCategory(list);
        }
        /// <summary>
        /// 获取当前用户菜单
        /// </summary>
        public List<Category> Categories => _authenticationService.GetMyCategories();
    }
}