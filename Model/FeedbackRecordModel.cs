///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-12 18:24:39
//CreateBy 		唐翔
//Content       反馈记录实体类
//////////////////////////////////////////////////////////////////
using System;

namespace Model
{
    /// <summary>
    /// 反馈记录实体
    /// </summary>
    public class FeedbackRecordModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public  int Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 反馈类型
        /// </summary>
        public int FeedbackType { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.EnumJsonConverter))]
        public Common.Keys.HandleStatus Status { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string HandlerRemark { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime? HandlerTime { get; set; }
        /// <summary>
        /// 处理者
        /// </summary>
        public int OperateId { get; set; }
    }
    /// <summary>
    /// 反馈记录视图实体
    /// </summary>
    public class v_FeedbackRecordModel
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
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 反馈类型
        /// </summary>
        public int FeedbackType { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.EnumJsonConverter))]
        public Common.Keys.HandleStatus Status { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string HandlerRemark { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime? HandlerTime { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperateId { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 反馈类型名称
        /// </summary>
        public string FeedbackTypeName { get; set; }
        /// <summary>
        /// 学生登录名
        /// </summary>
        public string StudentLoginName { get; set; }
        /// <summary>
        /// 操作管理员姓名
        /// </summary>
        public string OperateManagerName { get; set; }
        #endregion
    }
}
