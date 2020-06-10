///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-27 17:22:29
//CreateBy 		唐翔
//Content       邮箱账号实体视图类
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 邮箱账号实体视图类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class v_EmailAccountModel
    {
        #region 原表字段
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// smtp邮箱服务器地址，端口默认25
        /// <para>ps:smtp.qq.com:25</para>
        /// </summary>
        public string SmtpServer { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateId { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperateName { get; set; }
        #endregion
    }
}
