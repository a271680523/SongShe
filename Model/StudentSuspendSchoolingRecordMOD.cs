using Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Extend;

namespace Model
{
    /// <summary>
    /// 学生休学记录表
    /// </summary>
    public class StudentSuspendSchoolingRecordMOD
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 时区ID
        /// </summary>
        public int TimeZoneId { get; set; }
        /// <summary>
        /// 休学类型
        /// </summary>
        public int SuspendType { get; set; }
        /// <summary>
        /// 增加学生产品学籍天数
        /// </summary>
        public int AddStudentProductLimitDay { get; set; }
        /// <summary>
        /// 使用休学时长
        /// </summary>
        public int UseLeaveCount { get; set; }
        /// <summary>
        /// 剩余休学时长
        /// </summary>
        public int RestOfLeaveCount { get; set; }
        /// <summary>
        /// 休学后产品结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? ProductEndTime { get; set; }
        /// <summary>
        /// 休学状态
        /// </summary>
        public int Status { get; set; }
    }

    public class v_StudentSuspendSchoolingRecordMOD
    {
        #region 原表字段
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 时区ID
        /// </summary>
        public int TimeZoneId { get; set; }
        /// <summary>
        /// 休学类型
        /// </summary>
        public int SuspendType { get; set; }
        /// <summary>
        /// 增加学生产品学籍天数
        /// </summary>
        public int AddStudentProductLimitDay { get; set; }
        /// <summary>
        /// 使用休学时长
        /// </summary>
        public int UseLeaveCount { get; set; }
        /// <summary>
        /// 剩余休学时长
        /// </summary>
        public int RestOfLeaveCount { get; set; }
        /// <summary>
        /// 休学后产品结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? ProductEndTime { get; set; }
        /// <summary>
        /// 休学状态
        /// </summary>
        public int Status { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 学生名称
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 时区信息
        /// </summary>
        public TimeZoneModel TimeZone { get; set; }
        /// <summary>
        /// 状态值
        /// </summary>
        public string StatusName { get; set; }

        public DateTime StartTimeByTimeZone => StartTime.ConvertTime(TimeZone?.TimeZoneInfoId);
        public DateTime EndTimeByTimeZone => EndTime.ConvertTime(TimeZone?.TimeZoneInfoId);
        /// <summary>
        /// 是否可以被删除 结束时间未超过
        /// </summary>
        public bool IsCanDelete => EndTime > DateTime.UtcNow;

        #endregion
    }
}
