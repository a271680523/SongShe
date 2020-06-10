//////////////////////////////////////////////////////////////////
//CreateTime	2018-1-29 16:35:25
//CreateBy 		唐翔
//Content       时区信息实体类
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common;
using System.Linq;

namespace Model
{
    /// <summary>
    /// 时区信息实体类
    /// </summary>
    public class TimeZoneModel
    {
        public int ID { get; set; }
        /// <summary>
        /// TimeZoneInfo时区ID
        /// </summary>
        public string TimeZoneInfoId { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地区英语名称
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// UTC时区小时值
        /// </summary>
        public double UTCTotalHours { get; set; }

        /// <summary>
        /// 时区UTC值
        /// </summary>
        public double UTCValue => UTCTotalHours;
        /// <summary>
        /// UTC显示值 UTC+8:00
        /// </summary>
        public string UTCValueName { get; set; }
        /// <summary>
        /// 地区显示英语名称 UTC+8:00(EnglishName)
        /// </summary>
        public string ShowEnglishName => UTCValueName + (EnglishName.IsNullOrWhiteSpace() ? "" : " (" + EnglishName + ")");
        /// <summary>
        /// 是否具有夏令时
        /// </summary>
        public bool IsDST { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(true)]
        public bool IsEnable { get; set; }
    }
}
