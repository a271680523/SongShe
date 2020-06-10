///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-27 17:22:29
//CreateBy 		唐翔
//Content       邮箱账号实体类
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 邮箱邮件实体类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class v_EmailMailModel
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
        /// 邮件类型
        /// <para>0发送 1接收</para>
        /// </summary>
        public int MailType { get; set; }
        /// <summary>
        /// 发送人邮箱
        /// </summary>
        public string FromEmail { get; set; }
        /// <summary>
        /// 发送人名称
        /// </summary>
        public string FromName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容格式
        /// <para>0文本 1HTML</para>
        /// </summary>
        public int ContentType { get; set; }
        /// <summary>
        /// 邮箱账号ID
        /// </summary>
        public int EmailAccountId { get; set; }
        /// <summary>
        /// 状态
        /// <para>0等待发送 1发送成功 2发送失败 3已接收</para>
        /// </summary>
        public int MailStatus { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 指定发送时间 为空表示即时发送
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime? SpecifiedSendTime { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 
        /// </summary>
        public string MailTypeName => MailType == 0 ? "发送" : MailType == 1 ? "接收" : "";

        public string ContentTypeName => ContentType == 0 ? "文本" : ContentType == 1 ? "HTML" : "";

        public string MailStatusName => MailStatus == 0 ? "等待发送" : MailStatus == 1 ? "发送成功" : MailStatus == 2 ? "发送失败" : MailStatus == 3 ? "接收成功" : "";

        #endregion
    }
}
