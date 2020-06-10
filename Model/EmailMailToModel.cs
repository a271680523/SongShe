///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-27 17:22:29
//CreateBy 		唐翔
//Content       邮箱邮件接收人实体类
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 邮箱邮件接收人实体类
    /// </summary>
    public class EmailMailToModel
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
        /// 邮箱邮件ID
        /// </summary>
        public int EmailRecordId { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
