using System;

namespace Model
{
    /// <summary>
    /// 课程老师黑名单视图
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class v_CourseManagerBlackListMOD
    {
        #region 原字段
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
        #endregion

        #region 扩展字段
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentChinaName { get; set; }
        /// <summary>
        /// 学生英文名
        /// </summary>
        public string StudentEnglishName { get; set; }
        /// <summary>
        /// 学生账号
        /// </summary>
        public string StudentLoginName { get; set; }
        /// <summary>
        /// 任课老师姓名
        /// </summary>
        public string CourseManagerName { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperateName { get; set; }
        #endregion
    }
}
