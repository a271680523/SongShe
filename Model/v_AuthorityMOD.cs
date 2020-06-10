using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 权限信息视图
    /// </summary>
    public class v_AuthorityMOD
    {
        /// <summary>
        /// 权限信息
        /// </summary>
        public AuthorityModel Authority = new AuthorityModel();
        /// <summary>
        /// 拥有的菜单集合
        /// </summary>
        public List<MenuExtendModel> MenuExtend = new List<MenuExtendModel>();
        /// <summary>
        /// 拥有的操作方法集合
        /// </summary>
        public List<MenuActionInfoModel> MenuActionInfoList { get; set; }
        //public List<>

        public class MenuExtendModel
        {
            #region 基础字段
            /// <summary>
            /// 菜单ID
            /// </summary>
            [Display(Name = "菜单ID")]
            public int ID { get; set; }
            /// <summary>
            /// 菜单名称
            /// </summary>
            [Display(Name = "菜单名称")]
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
            public string MenuImg { get; set; }
            #endregion

            #region 扩展字段
            public bool IsAuthority { get; set; } 
            #endregion
        }
    }
}
