///////////////////////////////////////////////////////////////////
//CreateTime	2018年7月28日17:08:09
//CreateBy 		唐
//Content       扩展JsonConverter类
//////////////////////////////////////////////////////////////////
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Common.ExtendAttribute;
using Newtonsoft.Json.Serialization;

//using System.Web.SessionState;
namespace Common
{
    /// <summary>
    /// 扩展JsonConverter
    /// </summary>
    public class ExtendJsonConverter
    {
        /// <summary>
        /// 时间对象值扩展
        /// </summary>
        public class DateTimeJsonConverter : JsonConverter
        {
            public DateTimeJsonConverter() : base()
            {

            }
            private readonly Type[] _types;

            public DateTimeJsonConverter(params Type[] types)
            {
                this._types = types;
            }
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return existingValue?.ToString().ToDateTime(new DateTime());
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }
                var timeZoneInfoId = HttpContext.Current?.Session[Keys.TimeZoneInfoIdName];
                if (timeZoneInfoId != null)
                {
                    DateTime time = (DateTime)value;
                    if (time.Kind == DateTimeKind.Local)
                    {
                        writer.WriteValue(time);
                        return;
                    }
                    writer.WriteValue(time.ConvertTime(timeZoneInfoId.ToString()));
                    return;
                }
                writer.WriteValue(value.ToString());
            }
        }
        /// <summary>
        /// 枚举值扩展JSON序列化处理
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

}
