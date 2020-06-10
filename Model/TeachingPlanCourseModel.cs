///////////////////////////////////////////////////////////////////
//CreateTime	2018-2-2 15:56:51
//CreateBy 		唐翔
//Content       教案课程内容实体类
//////////////////////////////////////////////////////////////////
using System;

namespace Model
{
    /// <summary>
    /// 教案课程内容实体类
    /// </summary>
    public class TeachingPlanCourseModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ManagerId { get; set; }
        /// <summary>
        /// 教案ID
        /// </summary>
        public int TeachingPlanId { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 课程内容
        /// </summary>
        public string Content { get; set; }
    }
}
