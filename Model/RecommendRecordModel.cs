///////////////////////////////////////////////////////////////////
//CreateTime	2018-2-8 14:34:57
//CreateBy 		唐翔
//Content       推荐记录实体类
//////////////////////////////////////////////////////////////////
using System;
using Common;
using Common.Extend;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 推荐记录实体类
    /// </summary>
    public class RecommendRecordModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 推荐人姓名
        /// </summary>
        public string RecommendName { get; set; }
        /// <summary>
        /// 推荐人电话
        /// </summary>
        public string RecommendPhone { get; set; }
        /// <summary>
        /// 推荐人邮箱
        /// </summary>
        public string RecommendEmail { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 推荐状态
        /// </summary>
        public Keys.HandleStatus Status { get; set; }
        /// <summary>
        /// 推荐处理结果
        /// </summary>
        public string HandlerRemark { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? HandlerTime { get; set; }
    }
    /// <summary>
    /// 推荐记录实体类
    /// </summary>
    public class v_RecommendRecordModel
    {
        #region 原表字段
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 推荐人姓名
        /// </summary>
        public string RecommendName { get; set; }
        /// <summary>
        /// 推荐人电话
        /// </summary>
        public string RecommendPhone { get; set; }
        /// <summary>
        /// 推荐人邮箱
        /// </summary>
        public string RecommendEmail { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 推荐状态
        /// </summary>
        public Keys.HandleStatus Status { get; set; }
        /// <summary>
        /// 推荐处理结果
        /// </summary>
        public string HandlerRemark { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? HandlerTime { get; set; }
        #endregion

        #region 扩展展示字段

        /// <summary>
        /// 处理状态名称
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(EnumJsonConverter))]
        public Keys.HandleStatus StatusName => Status;
        /// <summary>
        /// 学生登录名
        /// </summary>
        public string StudentLoginName { get; set; }
        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperateName { get; set; }
        #endregion
    }
}
