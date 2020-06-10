using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 选课界面课程信息实体
    /// </summary>
    public class v_SelectCourseRecordMOD
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 任课老师
        /// </summary>
        public int CourseManagerID { get; set; }
        /// <summary>
        /// 是否主管老师
        /// </summary>
        public bool IsSupervisor { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime end { get; set; }
        /// <summary>
        /// 标题，即任课老师姓名
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 是否本人预约
        /// </summary>
        public bool IsMe { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public string backgroundColor { get; set; }
        /// <summary>
        /// 课程状态
        /// </summary>
        public int CourseStatus { get; set; }
        /// <summary>
        /// 选课学生
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public string textColor { get; set; }
        /// <summary>
        /// 边框颜色
        /// </summary>
        public string borderColor { get; set; }
    }
}
