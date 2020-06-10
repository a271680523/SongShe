using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Common.Extend
{
    /// <summary>
    /// 扩展JSON序列化和反序列化时对时间对象的处理
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
}
