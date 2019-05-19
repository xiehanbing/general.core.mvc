using System;
using General.Core.Librs;

namespace General.Framework.Menu
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class FunctionAttribute:Attribute
    {
        public FunctionAttribute()
        {

        }

        public FunctionAttribute(string name, bool isMenu, int sort = 100)
        {
            Name = name;
            IsMenu = isMenu;
            Sort = sort;
        }

        public FunctionAttribute(string name, string cssClass, int sort = 100)
        {
            Name = name;
            CssClass = cssClass;
            Sort = sort;
        }

        public FunctionAttribute(string name, bool isMenu, string cssClass, int sort = 100)
        {
            Name = name;
            CssClass = cssClass;
            Sort = sort;
            IsMenu = isMenu;
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string  Name{ get; set; }
        /// <summary>
        /// 是否为菜单
        /// </summary>
        public bool  IsMenu { get; set; }
        /// <summary>
        /// 系统资源 唯一标示
        /// </summary>
        public string SysResource { get; set; }
        /// <summary>
        /// 资源id 简化的id
        /// </summary>
        public string ResourceId {
            get
            {
                if (!string.IsNullOrEmpty(SysResource))
                {
                    return EncryptorHelper.GetMd5(SysResource);
                }

                return "";
            } }
        /// <summary>
        /// 父级资源  唯一标示
        /// </summary>
        public string FatherResource { get; set; }
        /// <summary>
        /// 父级资源id
        /// </summary>
        public string FatherId
        {
            get
            {
                if (!string.IsNullOrEmpty(FatherResource))
                {
                    return EncryptorHelper.GetMd5(FatherResource);
                }

                return "";
            }
        }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// class 样式类
        /// </summary>
        public string CssClass { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}