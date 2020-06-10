using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extend;

namespace Model
{
    /// <summary>
    /// 管理员课程表视图实体
    /// </summary>
    public class v_CurriculumManagerMOD
    {
        #region 原字段
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
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
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int EditManagerID { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 修改管理员姓名
        /// </summary>
        public string EditManagerName { get; set; }
        /// <summary>
        /// 时区信息
        /// </summary>
        public TimeZoneModel TimeZone { get; set; }
        /// <summary>
        /// 所处时区的课程表集合
        /// </summary>
        public List<CurricumModel> CurriculumList
        {
            get => CurricumModel.GetCurricumListByStrCurricum(Curriculum, TimeZone?.TimeZoneInfoId);
            set => Curriculum = CurricumModel.GetStrCurricumByCurricumList(value, TimeZone?.TimeZoneInfoId);
        }
        #endregion
    }
}
