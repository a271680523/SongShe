using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Extend
{
    /// <summary>
    /// 扩展JSON序列化和反序列化时对枚举对象的处理
    /// </summary>
    public class EnumJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        /// <summary>
        /// 反序列化处理
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return existingValue;
        }
        /// <summary>
        /// 序列化处理
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            string strValue = string.Empty;
            Type type = value.GetType();
            System.Reflection.FieldInfo fd = type.GetField(value.ToString());
            if (fd != null)
            {
                object[] attrs = fd.GetCustomAttributes(typeof(LanguageNameAttribute), false);
                foreach (LanguageNameAttribute attr in attrs)
                {
                    switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                    {
                        case "zh-CN":
                            if (!attr.Name.IsNullOrWhiteSpace())
                                strValue = attr.Name;
                            break;
                        case "en-US":
                            if (!attr.EnglishName.IsNullOrWhiteSpace())
                                strValue = attr.EnglishName;
                            break;
                    }
                    if (!strValue.IsNullOrWhiteSpace())
                    {
                        writer.WriteValue(strValue);
                        return;
                    }
                }
            }
            writer.WriteValue(value.ToString());
        }
    }
}
