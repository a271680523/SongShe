using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 课程老师黑名单实体
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class CourseManagerBlackListMOD
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
        /// 学生ID
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 任课老师ID
        /// </summary>
        public int CourseManagerId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateId { get; set; }
    }
}
