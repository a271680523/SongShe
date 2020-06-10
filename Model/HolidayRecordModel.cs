using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 假期记录实体
    /// </summary>
    public class HolidayRecordModel
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
        /// 假期名称
        /// </summary>
        public string HolidayName { get; set; }
        /// <summary>
        /// 假期天数
        /// </summary>
        public int HolidayDays { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperateId { get; set; }
        /// <summary>
        /// 所处时区
        /// </summary>
        public int TimeZoneId { get; set; }
    }
}
