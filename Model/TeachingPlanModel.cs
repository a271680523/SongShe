///////////////////////////////////////////////////////////////////
//CreateTime	2018-2-2 15:56:51
//CreateBy 		唐翔
//Content       教案实体类
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 教案实体类
    /// </summary>
    public class TeachingPlanModel
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
        /// 教案编号
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 教案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 教案详情
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 课时
        /// </summary>
        public int CourseCount { get; set; }
        /// <summary>
        /// 教材ID
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// 章节/教学内容
        /// </summary>
        public string Chapter { get; set; }
        /// <summary>
        /// 教案类型ID
        /// </summary>
        public int TeachingPlanTypeId { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 上一版本的教案ID
        /// </summary>
        public int BeforeId { get; set; }
    }

    /// <summary>
    /// 教案视图实体类
    /// </summary>
    public class v_TeachingPlanModel
    {
        #region 原表字段
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
        /// 教案编号
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 教案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 教案详情
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 课时
        /// </summary>
        public int CourseCount { get; set; }
        /// <summary>
        /// 教材ID
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// 章节/教学内容
        /// </summary>
        public string Chapter { get; set; }
        /// <summary>
        /// (教案类型/课型)的ID
        /// </summary>
        public int TeachingPlanTypeId { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 上一版本的教案ID
        /// </summary>
        public int BeforeId { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 教材名称
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 教案课型名称
        /// </summary>
        public string TeachingPlanTypeName { get; set; }
        /// <summary>
        /// 课时内容集合
        /// </summary>
        public List<TeachingPlanCourseModel> CourseContentList { get; set; } = new List<TeachingPlanCourseModel>();
        #endregion
    }
}
