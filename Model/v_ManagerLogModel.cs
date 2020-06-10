using System;

namespace Model
{
    /// <summary>
    /// 日志视图实体
    /// <para>创建人:唐翔</para>
    /// <para>创建时间:2018-1-10 15:59:13</para>
    /// </summary>
    public class v_ManagerLogModel
    {
        #region 原字段
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ManagerID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public ManagerLogType LogType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }
        #endregion
        #region 扩展字段
        /// <summary>
        /// 日志类型名称
        /// </summary>
        public string LogTypeName { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string ManagerName { get; set; } 
        #endregion
    }
}
