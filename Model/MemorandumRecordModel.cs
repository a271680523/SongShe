///////////////////////////////////////////////////////////////////
//CreateTime	2018-3-6 22:37:57
//CreateBy 		唐翔
//Content       备忘录记录实体类
//////////////////////////////////////////////////////////////////
using System;

namespace Model
{
    /// <summary>
    /// 备忘录记录实体类
    /// </summary>
    public class MemorandumRecordModel
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
        /// 学生ID
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ManagerId { get; set; }
        /// <summary>
        /// 备忘录类型
        /// </summary>
        public int MemorandumType { get; set; }
        /// <summary>
        /// 备忘录类型
        /// </summary>
        public string Content { get; set; }
    }
    /// <summary>
    /// 备忘录记录实体视图类
    /// </summary>
    public class v_MemorandumRecordModel
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
        /// 管理员ID
        /// </summary>
        public int ManagerId { get; set; }
        /// <summary>
        /// 备忘录类型
        /// </summary>
        public int MemorandumType { get; set; }
        /// <summary>
        /// 备忘录类型
        /// </summary>
        public string Content { get; set; }
        #endregion

        #region 扩展字段

        /// <summary>
        /// 学生账号
        /// </summary>
        public string StudentLoginName { get; set; }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string ManagerName { get; set; }
        #endregion
    }
}
