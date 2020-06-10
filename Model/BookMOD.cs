using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 教材表
    /// </summary>
    public class BookMOD
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 教材名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 添加者
        /// </summary>
        public int ManagerID { get; set; }
    }
    /// <summary>
    /// 教材实体视图
    /// </summary>
    public class v_BookMOD
    {
        #region 原字段
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 教材名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 添加者
        /// </summary>
        public int ManagerID { get; set; }
        #endregion

        #region 扩展显示字段
        /// <summary>
        /// 发布者姓名
        /// </summary>
        public string ManagerName { get; set; }
        #endregion
    }
}
