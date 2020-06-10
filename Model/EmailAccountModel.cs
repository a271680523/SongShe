///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-27 17:22:29
//CreateBy 		唐翔
//Content       邮箱账号实体类
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Common;
using Common.Extend;

namespace Model
{
    /// <summary>
    /// 邮箱账号实体类
    /// </summary>
    public class EmailAccountModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 邮箱名称
        /// </summary>
        public string EmailName { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }
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
        /// <para>host:port</para>
        /// </summary>
        public string SmtpServer { get; set; }
        /// <summary>
        /// 邮箱服务器host地址
        /// </summary>
        public string Host => SmtpServer?.Split(':').FirstOrDefault();
        /// <summary>
        /// 邮箱服务器port端口号,默认25
        /// </summary>
        public int Port => SmtpServer?.Split(':').LastOrDefault().ToInt(25) ?? 25;
        /// <summary>
        /// 账号类型
        /// </summary>
        [DefaultValue(0)]
        public int AccountType { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [ForeignKey("OperateId")]
        public ManagerModel OperateManage { get; set; }
    }
}
