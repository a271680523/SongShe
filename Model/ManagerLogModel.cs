///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-10 16:10:15
//CreateBy 		唐翔
//Content       日志实体类
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum ManagerLogType
    {
        /// <summary>
        /// 系统操作日志
        /// </summary>
        System = 1,
        /// <summary>
        /// 管理员操作日志
        /// </summary>
        Manager = 2,
        /// <summary>
        /// 调试日志
        /// </summary>
        Debug = 3,
        /// <summary>
        /// 学生操作日志
        /// </summary>
        Student=4
    }
    /// <summary>
    /// 日志实体
    /// </summary>
    public class ManagerLogModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
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
        /// 日志标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }
    }
}