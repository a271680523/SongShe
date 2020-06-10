using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Common.Extend;
using Newtonsoft.Json;

namespace Common
{
    public static class StaticCommon
    {
        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="oValue"></param>
        /// <param name="defaultValue">默认值</param>
        public static int ToInt(this object oValue, int defaultValue = 0)
        {
            int iValue = 0;
            if (oValue != null && (!int.TryParse(oValue.ToString(), out iValue)))
                iValue = defaultValue;
            return iValue;
        }

        /// <summary>
        /// 转换为T实体类型
        /// </summary>
        /// <param name="oValue"></param>
        /// <param name="isNew"></param>
        public static T ToModel<T>(this object oValue, bool isNew = false) where T : new()
        {
            T tModel;
            try { tModel = (T)oValue; }
            catch { tModel = isNew ? new T() : default(T); }
            return tModel;
        }
        /// <summary>
        /// 转换为int类型,默认为0
        /// </summary>
        public static int ToInt(this string sValue)
        {
            int iValue = sValue.ToInt(0);
            return iValue;
        }

        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="defaultValue">默认值为0</param>
        public static int ToInt(this string sValue, int defaultValue)
        {
            if (!int.TryParse(sValue, out var iValue))
                iValue = defaultValue;
            return iValue;
        }

        /// <summary>
        /// 转换为decimal类型
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="defaultValue">默认值</param>
        public static decimal ToDecimal(this string sValue, decimal defaultValue = 0)
        {
            if (!decimal.TryParse(sValue, out var dValue))
                dValue = defaultValue;
            return dValue;
        }
        /// <summary>
        /// 转换为double类型
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="defaultValue">默认值</param>
        public static double ToDouble(this object sValue, double defaultValue = 0)
        {
            try { return Convert.ToDouble(sValue); } catch { return 0; }
        }
        /// <summary>
        /// 转换为bool类型
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public static bool ToBool(this object sValue)
        {
            try { return Convert.ToBoolean(sValue); } catch { return false; }
        }

        /// <summary>
        /// 转换为TimeZoneInfoId时区的时区时间
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="timeZoneInfoId">时区标识ID</param>
        public static DateTime ConvertTime(this DateTime datetime, string timeZoneInfoId)
        {
            if (timeZoneInfoId.IsNullOrWhiteSpace())
                return datetime;
            if (datetime.Kind == DateTimeKind.Local)
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(datetime, timeZoneInfoId);
            return TimeZoneInfo.ConvertTimeFromUtc(datetime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfoId));
        }
        /// <summary>
        /// 转换为TimeZoneInfoId时区的Utc时间
        /// </summary>
        /// <param name="datetime"></param>
        public static DateTime ConvertUtcTime(this DateTime datetime)
        {
            if (datetime.Kind != DateTimeKind.Local)
                return datetime;
            return TimeZoneInfo.ConvertTimeToUtc(datetime);
        }
        /// <summary>
        /// 转换为TimeZoneInfoId时区的Utc时间
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="timeZoneInfoId">时区标识ID</param>
        public static DateTime ConvertUtcTime(this DateTime datetime, string timeZoneInfoId)
        {
            if (timeZoneInfoId.IsNullOrWhiteSpace())
                return datetime;
            if (datetime.Kind == DateTimeKind.Utc)
                return datetime;
            return TimeZoneInfo.ConvertTimeToUtc(datetime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfoId));
        }
        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="defaultValue">默认值</param>
        public static DateTime ToDateTime(this object sValue, DateTime defaultValue)
        {
            try { return Convert.ToDateTime(sValue); } catch { return defaultValue; }
        }

        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="sValue"></param>
        public static DateTime? ToDateTime(this string sValue)
        {
            if (DateTime.TryParse(sValue, out var dValue))
                return dValue;
            return null;
        }

        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="timeZone">当前时间的时区</param>
        public static DateTime? ToUtcDateTime(this string sValue, double timeZone)
        {
            if (!DateTime.TryParse(sValue, out var dValue))
                return null;
            return dValue.AddHours(-timeZone);
        }
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="sValue">要测试的字符串</param>
        /// <returns>如果 true 参数为 value 或空字符串 ("")，则为 null；否则为 false。</returns>
        public static bool IsNullOrEmpty(this string sValue)
        {
            return string.IsNullOrEmpty(sValue);
        }
        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="sValue">要测试的字符串</param>
        /// <returns>如果 true 参数为 value 或 null，或者如果 System.String.Empty 仅由空白字符组成，则为 value。</returns>
        public static bool IsNullOrWhiteSpace(this string sValue)
        {
            return string.IsNullOrWhiteSpace(sValue);
        }

        public class DecimalJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (existingValue is double value)
                    return value.ToString("#.00");
                return existingValue?.ToString();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 将数据转换为JSON字符串
        /// </summary>
        /// <param name="objData">待转换数据</param>
        /// <param name="dateTimeFormat"></param>
        /// <returns>JSON字符串</returns>
        public static string ToJson(this object objData, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            JsonSerializerSettings json = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                MaxDepth = 10,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,//忽略引用循环中的对象而不序列化它们
                //DefaultValueHandling = DefaultValueHandling.Ignore,//忽略默认值列
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DynamicContractResolver()
                //DateFormatHandling=DateFormatHandling.IsoDateFormat,
                //DateParseHandling=DateParseHandling.DateTime,
                //DateFormatString=DateFormatHandling.MicrosoftDateFormat,
                //DateFormatHandling=DateFormatHandling.MicrosoftDateFormat
            };
            //JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            //{
            //    ////日期类型默认格式化处理  
            //    //json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            //    //json.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            //    ////空值处理  
            //    //json.NullValueHandling = NullValueHandling.Ignore;

            //    //高级用法九中的Bool类型转换 设置  
            //    json.Converters.Add(new DateTimeJsonConverter());

            //    return json;
            //});
            //if(objData.GetType())

            return JsonConvert.SerializeObject(objData, json);

            //return JsonConvert.SerializeObject(objData, Formatting.Indented, new DateTimeJsonConverter(objData.GetType()));
        }

        #region 扩展ToJsonResult
        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">待转换数据</param>
        /// <returns></returns>
        public static JsonResult ToJsonResult(this object data)
        {
            return ToJsonResult(data, null);
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        public static JsonResult ToJsonResult(this object data, string contentType)
        {
            return ToJsonResult(data, contentType, null);
        }
        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        public static JsonResult ToJsonResult(this object data, string contentType, Encoding contentEncoding)
        {
            return ToJsonResult(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        public static JsonResult ToJsonResult(this object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return ToJsonResult(data, contentType, contentEncoding, behavior, null);
        }
        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <param name="dateTimeFormat">DateTime格式</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        public static JsonResult ToJsonResult(this object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, string dateTimeFormat)
        {
            return new JsonResultExtend { Data = data, ContentType = contentType, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior, DateTimeFormat = dateTimeFormat };
        }
        #endregion

        /// <summary>
        /// 多行本文形式转换成HTML形式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHtml(this string str)
        {
            if (str.IsNullOrWhiteSpace())
                return str;
            return str.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
        }
        /// <summary>
        /// 判断是否是int值
        /// </summary>
        public static bool IsInt(this object oValue)
        {
            if (oValue == null)
                return false;
            if (int.TryParse(oValue.ToString(), out _))
                return true;
            return false;
        }
        /// <summary>
        /// 判断是否是decimal值
        /// </summary>
        public static bool IsDecimal(this object oValue)
        {
            if (oValue == null)
                return false;
            if (decimal.TryParse(oValue.ToString(), out _))
                return true;
            return false;
        }
        /// <summary>
        /// 判断是否是DateTime
        /// </summary>
        public static bool IsDateTime(this object oValue)
        {
            if (oValue == null)
                return false;
            if (DateTime.TryParse(oValue.ToString(), out _))
                return true;
            return false;
        }
        /// <summary>
        /// 默认时区标识值TimeZoneInfoId
        /// </summary>
        public static string LocalTimeZoneInfoId => TimeZoneInfo.Local.Id;
        /// <summary>
        /// 默认时区偏移量(小时)
        /// </summary>
        public static double LocalTimeZoneInfoOffset => TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).TotalHours;
        /// <summary>
        /// 获取Value  验证key是否存在 否则返回empty
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="defatultValue"></param>
        /// <returns></returns>
        public static string GetValue(this IDictionary<string, string> dic, string key, string defatultValue = "") => (dic.ContainsKey(key)) ? dic[key] : defatultValue;

        /// <summary>
        /// 获取最大连续时间数量
        /// </summary>
        /// <param name="dicDateTime"></param>
        /// <returns></returns>
        public static int GetMaxContins(IDictionary<DateTime, DateTime> dicDateTime)
        {
            int maxContins = 0;
            int contins = 0;
            DateTime time = dicDateTime.FirstOrDefault().Key;
            foreach (var item in dicDateTime)
            {
                if (time == item.Key)
                {
                    contins++;
                    time = item.Value;
                }
                else
                {
                    contins = 0;
                    if (contins > maxContins)
                    {
                        maxContins = contins;
                    }
                }
            }
            return maxContins;
        }
        /// <summary>
        /// 获取枚举的Description值，如果为空则返回该枚举值的Name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var attrDesc = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            if (attrDesc != null)
                return ((DescriptionAttribute)attrDesc).Description;
            return value.ToString();
        }
        /// <summary>
        /// 获取当前时间的本周第一天时间，周日为每周第一天
        /// </summary>
        /// <returns></returns>
        public static DateTime GetStartWeekDate()
        {
            return GetStartWeekDate(DateTime.Now);
        }
        /// <summary>
        /// 获取传入时间的当周第一天时间，周日为每周第一天
        /// </summary>
        /// <param name="time">所处周的时间</param>
        /// <returns></returns>
        public static DateTime GetStartWeekDate(DateTime time)
        {
            return time.AddDays(-time.DayOfWeek.ToString("d").ToInt()).Date;//本周周日的0点的时间
        }

        /// <summary>
        /// 是否由字母或数字组成
        /// </summary>
        /// <param name="strValue">判断值</param>
        public static bool IsLettersOrNumber(this string strValue)
        {
            return Regex.IsMatch(strValue, "^[0-9a-zA-Z]+$");
        }
        /// <summary>
        /// 是否由字母或数字或_组成,并且首字符为字母
        /// </summary>
        /// <param name="strValue">判断值</param>
        public static bool IsLettersOrNumberAndIndexLetters(this string strValue)
        {
            return Regex.IsMatch(strValue, "^[a-zA-Z][0-9a-zA-Z_]+$");
        }

        /// <summary>
        /// Email格式验证
        /// </summary>
        /// <param name="strValue">判断值</param>
        public static bool IsEmail(this string strValue)
        {
            return Regex.IsMatch(strValue, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        /// 密码等级 大写字母、小写字母、数字
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int PasswordLevel(this string pwd)
        {
            int count = 0;
            if (pwd.IsNullOrWhiteSpace())
                return count;
            if (Regex.IsMatch(pwd, "[A-Z]"))
                count++;
            if (Regex.IsMatch(pwd, "[0-9]"))
                count++;
            if (Regex.IsMatch(pwd, "[a-z]"))
                count++;
            return count;
        }
        /// <summary>
        /// 是否由纯数字组成
        /// </summary>
        /// <param name="strValue">判断值</param>
        public static bool IsNumber(this string strValue)
        {
            return Regex.IsMatch(strValue, @"^[0-9]+$");
        }
        /// <summary>
        /// 是否是内置类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBulitinType(this Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        /// <summary>
        /// 生成随机数对象
        /// </summary>
        public static Random Rand = new Random();

        /// <summary>
        /// 创建随机验证码字符
        /// </summary>
        /// <param name="codeLength">字符长度</param>
        /// <returns>随机验证码字符</returns>
        public static string RandNumber(int codeLength = 4)
        {
            if (codeLength <= 0)
                codeLength = 4;
            char[] charCode = new char[codeLength];
            char[] charRand = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T'
                 , 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l'
                 , 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' ,'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            for (int i = 0; i < codeLength; i++)
            {
                charCode[i] = charRand[Rand.Next(charRand.Length - 1)];
            }
            return new string(charCode);
        }
        /// <summary>
        /// 创建验证码图片
        /// 思路是使用GDI+创建画布，使用伪随机数生成器生成渐变画刷，然后创建渐变文字。
        /// </summary>
        /// <param name="verificationText">验证码字符串</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片长度</param>
        /// <returns>图片</returns>
        public static Bitmap CreateVerificationImage(string verificationText, int width, int height)
        {
            //var pen = new Pen(Color.Black);
            Font font = new Font("Arial", 14, FontStyle.Bold);
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            SizeF totalSizeF = g.MeasureString(verificationText, font);
            //PointF _startPointF = new PointF((width - _totalSizeF.Width) / 2, (height - _totalSizeF.Height) / 2);
            PointF startPointF = new PointF(0, (height - totalSizeF.Height) / 2);
            //随机数产生器
            //Random _random = new Random();
            g.Clear(Color.White);
            foreach (char str in verificationText)
            {
                Brush brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(Rand.Next(255), Rand.Next(255), Rand.Next(255)), Color.FromArgb(Rand.Next(255), Rand.Next(255), Rand.Next(255)));
                g.DrawString(str.ToString(), font, brush, startPointF);
                var curCharSizeF = g.MeasureString(str.ToString(), font);
                startPointF.X += curCharSizeF.Width;
            }
            g.Dispose();
            return bitmap;
        }
        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 获取扩展语言值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="languageType"></param>
        /// <returns></returns>
        public static string GetExtendLanguageName(this Enum obj, Keys.LanguageType languageType = Keys.LanguageType.ChinaName)
        {
            Type type = obj.GetType();
            FieldInfo fd = type.GetField(obj.ToString());
            if (fd == null)
                return obj.ToString();
            object[] attrs = fd.GetCustomAttributes(typeof(LanguageNameAttribute), false);
            foreach (LanguageNameAttribute attr in attrs)
            {
                switch (languageType)
                {
                    case Keys.LanguageType.ChinaName:
                        if (!attr.Name.IsNullOrWhiteSpace())
                            return attr.Name;
                        break;
                    case Keys.LanguageType.EnglishName:
                        if (!attr.EnglishName.IsNullOrWhiteSpace())
                            return attr.EnglishName;
                        break;
                }
            }
            return obj.ToString();
        }

        #region 获取用户的IP地址-全
        /// <summary>
        /// 获取用户的IP地址-全
        /// </summary>
        public static string GetRealIp
        {
            get
            {
                try
                {
                    string userIp = HttpContext.Current.Request.Headers["X-Forwarded-For"];
                    if (userIp != null && userIp.ToLower() != "unknown")
                    {
                        //X-Forwarded-For: client1, proxy1, proxy2    
                        string[] arrIp = userIp.Split(',');
                        userIp = arrIp[0];
                        if (arrIp.Length > 1)
                        {//如果第一组IP是10和168开头还有172.16-172.31（第二码区间在16-31之间）的话，就取第二组IP
                            if (userIp.IndexOf("10.", StringComparison.Ordinal) == 0 || userIp.IndexOf("192.168.", StringComparison.Ordinal) == 0 || (userIp.IndexOf("172.", StringComparison.Ordinal) == 0 && (userIp.Split('.').Length > 1 && Convert.ToInt32(userIp.Split('.')[1]) > 15 && Convert.ToInt32(userIp.Split('.')[1]) < 32)))
                                userIp = arrIp[1];
                        }
                    }
                    else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToLower() != "unknown")
                        userIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    else
                        userIp = HttpContext.Current.Request.UserHostAddress;
                    if (userIp == null || userIp == "::1") userIp = "127.0.0.1";
                    if (userIp.Length > 15)
                        userIp = userIp.Substring(0, 15);
                    Regex reip = new Regex("(\\d+).(\\d+).(\\d+).(\\d+)");
                    return reip.Replace(userIp, "$1.$2.$3.$4");
                }
                catch
                {
                    return "127.0.0.1";
                }
            }
        }
        #endregion

        #region 判断开始是否 http:// https:// 没有添加(默认为http://)
        /// <summary>
        /// 判断开始是否 http:// https:// 没有添加(默认为http://)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponseUrl(string url)
        {
            if (url.ToLower().StartsWith("http://") || url.ToLower().StartsWith("https://")) return url;
            url = string.Concat("http://", url);
            return url;
        }
        #endregion



        /// <summary>
        /// 类型是否是返回视图
        /// </summary>
        /// <param name="resultType"></param>
        /// <returns></returns>
        public static bool IsResultView(Type resultType)
        {
            if (typeof(ViewResultBase).IsAssignableFrom(resultType) || typeof(RedirectResult).IsAssignableFrom(resultType) || typeof(RedirectToRouteResult).IsAssignableFrom(resultType))//是否是ViewResultBase、RedirectResult、RedirectToRouteResult的子类
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T">数据源中数据的类型</typeparam>
        /// <param name="data">提供针对特定数据源（其中数据类型未未知）评估查询的功能</param>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页码大小</param>
        /// <returns></returns>
        public static List<T> ToPagedList<T>(this IQueryable<T> data, int pageIndex, int pageSize)
        {
            var skip = (pageIndex - 1) * pageSize;
            if (skip < 0) skip = 0;
            var take = pageSize;
            if (take < 0) take = 0;
            return data.Skip(skip).Take(take).ToList();
        }
        /// <summary>
        /// 获取请求的区域名称
        /// </summary>
        /// <returns></returns>
        public static Keys.AreaNameEnum GetAreaEnum()
        {
            HttpContext.Current.Request.RequestContext.RouteData.Values.TryGetValue("Area", out object area);
            var areaName = area?.ToString() ?? "";
            if (areaName == "")
                return Keys.AreaNameEnum.NullArea;
            Enum.TryParse<Keys.AreaNameEnum>(areaName, true, out var areaNameEnum);
            return areaNameEnum;
        }

    }
}
