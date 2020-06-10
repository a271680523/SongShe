///////////////////////////////////////////////////////////////////
//CreateTime	2018-2-8 14:34:57
//CreateBy 		唐翔
//Content       申请免费试读课程记录实体类
//////////////////////////////////////////////////////////////////
using System;
using Common;
using Common.Extend;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 申请免费试读课程记录实体类
    /// </summary>
    public class ApplyFreeProbationRecordModel
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
        /// 上课时间
        /// </summary>
        public string CourseTime { get; set; }
        /// <summary>
        /// 学习需求
        /// </summary>
        public string LearningNeeds { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public Keys.HandleStatus Status { get; set; }
        /// <summary>
        /// 申请处理结果
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
        public DateTime? EditTime { get; set; }
    }
    /// <summary>
    /// 申请免费试读课程记录实体视图类
    /// </summary>
    public class V_ApplyFreeProbationRecordModel
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
        /// 上课时间
        /// </summary>
        public string CourseTime { get; set; }
        /// <summary>
        /// 学习需求
        /// </summary>
        public string LearningNeeds { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public Keys.HandleStatus Status { get; set; }
        /// <summary>
        /// 申请处理结果
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
        public DateTime? EditTime { get; set; }
        #endregion

        #region 扩展展示字段

        /// <summary>
        /// 学生登录名
        /// </summary>
        public string StudentLoginName { get; set; }
        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperateName { get; set; }
        /// <summary>
        /// 处理状态名称
        /// </summary>
        [JsonConverter(typeof(EnumJsonConverter))]
        public Keys.HandleStatus StatusName => Status;
        #endregion
    }
}
