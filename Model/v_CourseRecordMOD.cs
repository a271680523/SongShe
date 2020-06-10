using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 扩展课程记录表实体
    /// </summary>
    public class v_CourseRecordMOD
    {
        #region 原表字段

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

        #endregion

        #region 扩展字段

        /// <summary>
        /// 任课管理员姓名
        /// </summary>
        public string CourseManagerName { get; set; }

        /// <summary>
        /// 发布者管理员姓名
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 是否主管老师课程
        /// </summary>
        public bool IsSupervisor { get; set; }
        /// <summary>
        /// 是否本人预约
        /// </summary>
        public bool IsMe { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentLoginName { get; set; }

        #endregion
    }
}
