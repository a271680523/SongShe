///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-27 17:22:29
//CreateBy 		唐翔
//Content       邮箱邮件内容模板实体类
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace Model
{
    /// <summary>
    /// 邮箱邮件内容模板实体类
    /// </summary>
    public class EmailMailContentMaterModel
    {
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
        /// 邮件内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容格式
        /// <para>0文本 1HTML</para>
        /// </summary>
        public Keys.MailContentType ContentType { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateId { get; set; }
    }
}
