using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class StudentCourseStatusMOD
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]//不自动增长
        public int StatusID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 课程状态中文名
        /// </summary>
        public string ChinaName { get; set; }
        /// <summary>
        /// 是否计算课时
        /// </summary>
        public bool IsEffective { get; set; }
        /// <summary>
        /// 课程状态英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 是否固定值
        /// </summary>
        public bool IsFixedValue { get; set; }
        /// <summary>
        /// 固定值名称
        /// </summary>
        public string FixedValueName { get; set; }
    }
}