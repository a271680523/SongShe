using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 扩展类型实体表
    /// </summary>
    public class ExtendTypeModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 添加
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 扩展类型ID 1反馈类型
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.EnumJsonConverter))]
        public Common.Keys.ExtendTypeId TypeId { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperateId { get; set; }
    }
}
