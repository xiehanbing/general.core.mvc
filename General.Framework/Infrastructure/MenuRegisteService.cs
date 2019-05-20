﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using General.Entity.Category;
using General.Services.Category;

namespace General.Framework.Infrastructure
{
    public class MenuRegisteService:IMenuRegisterService
    {
        private ICategoryService _categoryService;

        public MenuRegisteService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void InitRegister()
        {

        }
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void InitRegister()
        //{
        //    List<Entity.Category.Category> list = new List<Category>();
        //    FunctionManager.GetFunctionLists().ForEach(item =>
        //    {
        //        list.Add(new Category()
        //        {
        //            Name = item.Name,
        //            Action = item.Action,
        //            Controller = item.Controller,
        //            CssClass = item.CssClass,
        //            FatherResource = item.FatherResource,
        //            FatherId = item.FatherId,
        //            SysResource = item.SysResource,
        //            ResourceId = item.ResourceId,
        //            RouteName = item.RouteName,
        //            Sort = item.Sort,
        //            IsMenu = item.IsMenu,
        //            IsDisabled = item.IsDisabled
        //        });

        //    });

        //    _categoryService.InitCategory(list);
        //}
    }
}