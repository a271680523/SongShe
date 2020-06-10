///////////////////////////////////////////////////////////////////
//CreateTime	2018年7月28日17:08:09
//CreateBy 		唐
//Content       扩展JsonConverter类
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extend
{
    /// <summary>
    /// 此特性用于json序列化此字段时，是否忽略该字段，通过传入区域名称控制是否忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class JsonIgnoreByAreaNameAttribute : Attribute
    {
        public List<Keys.AreaNameEnum> AreaNameList;
        /// <summary>
        /// json序列化时根据区域判断是否忽略
        /// </summary>
        /// <param name="areaNames">区域名称</param>
        public JsonIgnoreByAreaNameAttribute(params Keys.AreaNameEnum[] areaNames)
        {
            AreaNameList = areaNames.ToList();
        }
    }
}
