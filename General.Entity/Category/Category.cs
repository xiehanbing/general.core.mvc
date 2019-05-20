using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entity.Category
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table("Category")]
    [Serializable]
    public class Category
    {
        public Category()
        {
            SysPermissions=new HashSet<SysPermission>();
        }
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public int  Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "Name is not null")]
        public string  Name { get; set; }
        /// <summary>
        /// 是否是菜单
        /// </summary>
        public bool IsMenu { get; set; }
        /// <summary>
        /// 系统资源 唯一标示
        /// </summary>
        public string  SysResource { get; set; }
        /// <summary>
        /// 资源id 简化的id
        /// </summary>
        public string  ResourceId { get; set; }
        /// <summary>
        /// 父级资源  唯一标示
        /// </summary>
        public string  FatherResource { get; set; }
        /// <summary>
        /// 父级资源id
        /// </summary>
        public string  FatherId { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string  Controller { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string  Action { get; set; }
        /// <summary>
        /// 路由名称
        /// </summary>
        public string  RouteName { get; set; }
        /// <summary>
        /// class 样式类
        /// </summary>
        public string  CssClass { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int  Sort { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
        public virtual ICollection<SysPermission> SysPermissions { get; set; }
    }
}