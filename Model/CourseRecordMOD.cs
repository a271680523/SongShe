using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    /// <summary>
    /// 课程安排记录表
    /// </summary>
    public class CourseRecordMOD
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 课程老师ID
        /// </summary>
        public int CourseManagerID { get; set; }
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
        /// 课时
        /// </summary>
        public int CourseCount { get; set; }
        /// <summary>
        /// 课程状态
        /// </summary>
        public int CourseStatus { get; set; }
        /// <summary>
        /// 发布者ID
        /// </summary>
        public int ManagerID { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
    }
}