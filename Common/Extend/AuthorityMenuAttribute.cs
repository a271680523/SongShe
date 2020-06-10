///////////////////////////////////////////////////////////////////
//CreateTime	2018年7月28日17:08:09
//CreateBy 		唐
//Content       扩展JsonConverter类
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extend
{
    /// <summary>
    /// 权限菜单信息，该特性用于控制是否有权限访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Struct, AllowMultiple = true)]
    public class AuthorityMenuAttribute : Attribute
    {
        /// <summary>
        /// 权限菜单扩展属性
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        public AuthorityMenuAttribute(int menuId)
        {
            MenuId = menuId;
            Version = 1.0;
        }

        /// <summary>
        /// 权限菜单扩展属性
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="menuName">菜单名称</param>
        public AuthorityMenuAttribute(int menuId, string menuName)
        {
            MenuId = menuId;
            MenuName = menuName;
            Version = 1.0;
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public double Version;
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

    }
}
