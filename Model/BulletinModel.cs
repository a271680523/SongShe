///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-26 00:14:00
//CreateBy 		唐翔
//Content       公告实体类
//////////////////////////////////////////////////////////////////
using System;

namespace Model
{
    /// <summary>
    /// 公告实体类
    /// </summary>
    public class BulletinModel
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
        /// 教案编号，每次修改版本号增加，教案编号不改变
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 展示开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 展示结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public  DateTime EndTime { get; set; }
        /// <summary>
        /// 优先值
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 状态 0不显示 1显示
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateId { get; set; }
    }
}
