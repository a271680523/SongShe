using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 管理员课程表
    /// </summary>
    public class CurriculumManagerMOD
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ManagerID { get; set; }
        /// <summary>
        /// 课程表 ,1-7,1-8,
        /// </summary>
        public string Curriculum { get; set; }
        /// <summary>
        /// 所属时区
        /// </summary>
        public int TimeZoneID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int EditManagerID { get; set; }
    }
}
