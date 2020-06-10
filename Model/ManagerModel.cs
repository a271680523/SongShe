using Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Extend;

namespace Model
{
    /// <summary>
    /// 管理员实体类
    /// </summary>

    [Table("ManagerModel")]
    public class ManagerModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 权限租ID
        /// </summary>
        public int AuthorityID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 操作管理员ID
        /// </summary>
        public int Operate { get; set; }
        /// <summary>
        /// 时区ID
        /// </summary>
        public int TimeZone { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 座机电话
        /// </summary>
        public string LandlinePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; }
        /// <summary>
        /// Skype
        /// </summary>
        public string Skype { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
    }
    /// <summary>
    /// 管理员视图实体类
    /// </summary>
    public class v_ManagerMOD
    {
        #region 原实体
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 权限租ID
        /// </summary>
        public int AuthorityID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 操作管理员ID
        /// </summary>
        public int Operate { get; set; }
        /// <summary>
        /// 所处时区
        /// </summary>
        public int TimeZone { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 座机电话
        /// </summary>
        public string LandlinePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; }
        /// <summary>
        /// Skype
        /// </summary>
        public string Skype { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 权限名称
        /// </summary>
        public string AuthorityName { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string OperateName { get; set; }
        /// <summary>
        /// 时区信息
        /// </summary>
        public TimeZoneModel timeZone { get; set; }
        #endregion
    }
}