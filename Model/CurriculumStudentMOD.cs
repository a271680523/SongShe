using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 学生课程表
    /// </summary>
    public class CurriculumStudentMOD
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 课程老师ID
        /// </summary>
        public int CourseManagerID { get; set; }
        /// <summary>
        /// 课程表 ,1-7,1-8,
        /// </summary>
        public string Curriculum { get; set; }
        /// <summary>
        /// 所属时区
        /// </summary>
        public int TimeZoneID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int EditManagerID { get; set; }
    }
}
