using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Model
{

    /// <summary>
    /// 菜单实体类
    /// </summary>
    public class MenuModel
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        [Key]
        [Display(Name = "菜单ID")]
        public int ID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Display(Name = "菜单名称")]
        [DefaultValue("")]
        public string MenuName { get; set; }
        /// <summary>
        /// 上级菜单ID
        /// </summary>
        [Display(Name = "上级菜单ID")]
        public int ParentID { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [Display(Name = "链接地址")]
        [DefaultValue("")]
        public string LinkUrl { get; set; }
        /// <summary>
        /// 菜单级别
        /// </summary>
        [Display(Name = "菜单级别")]
        public int Grade { get; set; }
        /// <summary>
        /// 优先值
        /// </summary>
        [Display(Name = "优先值")]
        public int Sort { get; set; }
        /// <summary>
        /// 菜单图片
        /// </summary>
        [Display(Name = "菜单图片")]
        [DefaultValue("")]
        public string MenuImg { get; set; }
        /// <summary>
        /// 是否权限
        /// </summary>
        [DefaultValue(false)]
        public bool IsAuthority { get; set; }

        /// <summary>
        /// 拥有的操作方法集合
        /// </summary>
        public virtual List<MenuActionInfoModel> MenuActionInfoList { get; set; }
    }
    ///// <summary>
    ///// 菜单权限实体类
    ///// </summary>
    //public class MenuAuthorityModel
    //{
    //    /// <summary>
    //    /// ID
    //    /// </summary>
    //    public int Id { get; set; }
    //    /// <summary>
    //    /// 菜单ID
    //    /// </summary>
    //    public int MenuId { get; set; }
    //    [ForeignKey("MenuId")]
    //    public MenuModel Menu { get; set; }
    //    /// <summary>
    //    /// 上级菜单权限ID
    //    /// </summary>
    //    public int ParentMenuAuthorityId { get; set; }
    //    /// <summary>
    //    /// 菜单权限名称
    //    /// </summary>
    //    public string MenuItemName { get; set; }
    //}

    /// <summary>
    /// 菜单权限拥有的控制器方法实体类
    /// </summary>
    public class MenuActionInfoModel
    {
        /// <summary>
        /// 菜单权限ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public int MenuId { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        [JsonIgnore]
        [ForeignKey("MenuId")]
        public MenuModel Menu { get; set; }
        /// <summary>
        /// 操作方法信息ID
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public int ActionInfoId { get; set; }
        /// <summary>
        /// 操作方法信息
        /// </summary>
        [JsonIgnore]
        [ForeignKey("ActionInfoId")]
        public virtual ActionInfoModel ActionInfo { get; set; }
    }

    /// <summary>
    /// 操作方法信息实体类
    /// </summary>
    public class ActionInfoModel
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DefaultValue("")]
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DefaultValue("")]
        public string Remark { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 操作方法的名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 控制器的完全限定名称，包括其命名空间，但不包括程序集
        /// <para>暂定可选</para>
        /// </summary>
        public string ControllerFullName { get; set; }
        /// <summary>
        /// 方法返回类型
        /// </summary>
        public string ActionResultType { get; set; }
        /// <summary>
        /// 上级操作方法ID
        /// </summary>
        [DefaultValue(null)]
        public int? ParentId { get; set; }
        /// <summary>
        /// 上级操作方法
        /// </summary>
        [ForeignKey("ParentId")]
        [JsonIgnore]
        public virtual ActionInfoModel ParentActionInfo { get; set; }
        /// <summary>
        /// 下级操作方法集合
        /// </summary>
        [JsonIgnore]
        public virtual List<ActionInfoModel> NextLevelActionInfo { get; set; }
        ///// <summary>
        ///// 所属菜单集合
        ///// </summary>
        //public virtual List<MenuModel> MenuList { get; set; }

        /// <summary>
        /// 所属菜单集合
        /// </summary>
        public virtual List<MenuActionInfoModel> MenuList { get; set; }
    }
    /// <summary>
    /// 菜单视图实体
    /// </summary>
    public class v_MenuModel
    {
        #region 原表字段
        /// <summary>
        /// 唯一编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 菜单级别
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 优先值
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 菜单图片
        /// </summary>
        public string MenuImg { get; set; }
        /// <summary>
        /// 是否权限
        /// </summary>
        public bool IsAuthority { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 上级菜单名称
        /// </summary>
        public string ParentName { get; set; }
        #endregion
    }
}