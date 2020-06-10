using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Model
{
    /// <summary>
    /// 产品表
    /// </summary>
    public class ProductMOD
    {
        public ProductMOD() { this.IsEnable = true; }
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 频率
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// 学籍时长
        /// </summary>
        public int LimitDate { get; set; }
        /// <summary>
        /// 总课时
        /// </summary>
        public int CourseCount { get; set; }
        /// <summary>
        /// 可休学时长
        /// </summary>
        public int LeaveCount { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal PriceMoney { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}