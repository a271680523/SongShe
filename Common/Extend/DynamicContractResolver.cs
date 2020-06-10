using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Extend
{
    /// <summary>
    /// 自定义返回需要输出的字段，属性ContractResolver
    /// </summary>
    public class DynamicContractResolver : DefaultContractResolver
    {
        private Keys.AreaNameEnum AreaName { get; }

        public DynamicContractResolver()
        {
            AreaName = StaticCommon.GetAreaEnum();
        }

        public DynamicContractResolver(Keys.AreaNameEnum areaName)
        {
            AreaName = areaName;
        }
        /// <summary>
        /// 处理需要序列化的列集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            //判断是否有需要忽略字段
            properties = properties.Where(d => !d.AttributeProvider.GetAttributes(typeof(JsonIgnoreByAreaNameAttribute), false).Any(e => ((JsonIgnoreByAreaNameAttribute)e).AreaNameList.Contains(AreaName))).ToList();
            return properties;
        }
    }
}
