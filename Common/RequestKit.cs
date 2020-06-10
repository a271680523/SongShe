using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    /// <summary>
    /// Web请求常用方法HttpContext.Current.Request
    /// </summary>
    public static class RequestKit
    {
        #region GetQueryStringValue

        /// <summary>
        /// 获取地址栏传递参数值-自带默认值
        /// 新增于2011-5-1
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <returns>地址栏参数值</returns>
        public static T GetQueryStringValue<T>(string keyname)
        {
            string value = HttpContext.Current.Request.QueryString[keyname];
            Type type = typeof(T);
            object result;
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }
        /// <summary>
        /// 获取地址栏传递参数值 (T为string,char,bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>地址栏参数值</returns>
        public static T GetQueryStringValue<T>(string keyname, T defaultValue)=> ConvertValue<T>(HttpContext.Current.Request.QueryString[keyname], defaultValue);
        /// <summary>
        /// 获取地址栏传递参数值(可为null) (T只能为bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// (string,char不需要使用Nullable类型)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>地址栏参数值</returns>
        public static T? GetQueryStringValue<T>(string keyname, T? defaultValue) where T : struct=> ConvertValue<T>(HttpContext.Current.Request.QueryString[keyname], defaultValue);
        #endregion

        #region GetFormValue

        /// <summary>
        /// 获取表单传递参数(Post方式)-自带默认值
        /// 新增于2011-5-1
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <returns></returns>
        public static T GetFormValue<T>(string keyname)
        {
            string value = HttpContext.Current.Request.Form[keyname];
            Type type = typeof(T);
            object result;
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }

        /// <summary>
        /// 获取表单传递参数(Post方式) (T为string,char,bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>表单值</returns>
        public static T GetFormValue<T>(string keyname, T defaultValue)=> ConvertValue<T>(HttpContext.Current.Request.Form[keyname], defaultValue);
        /// <summary>
        /// 获取表单传递参数(Post方式,可为null) (T只能为bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// (string,char不需要使用Nullable类型)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T? GetFormValue<T>(string keyname, T? defaultValue) where T : struct=> ConvertValue<T>(HttpContext.Current.Request.Form[keyname], defaultValue);
        #endregion

        #region GetParamsValue

        /// <summary>
        /// 获取表单、地址栏等传递参数-自带默认值
        /// 新增于2011-5-1
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <returns>表单、地址栏参数值</returns>
        public static T GetParamsValue<T>(string keyname)
        {
            string value = HttpContext.Current.Request.Params[keyname];
            Type type = typeof(T);
            object result;
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }
        /// <summary>
        /// 获取表单、地址栏等传递参数 (T为string,char,bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>表单、地址栏参数值</returns>
        public static T GetParamsValue<T>(string keyname, T defaultValue)=> ConvertValue<T>(HttpContext.Current.Request.Params[keyname], defaultValue);
        /// <summary>
        /// 获取表单、地址栏等传递参数(可为null) (T只能为bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// (string,char不需要使用Nullable类型)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="keyname">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>表单、地址栏参数值</returns>
        public static T? GetParamsValue<T>(string keyname, T? defaultValue) where T : struct=> ConvertValue<T>(HttpContext.Current.Request.Params[keyname], defaultValue);
        #endregion

        #region ConvertValue
        /// <summary>
        /// 值类型转换 (T为string,char,bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="sourceValue">源值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T ConvertValue<T>(object sourceValue, T defaultValue)
        {
            //ToString//ToChar                          字符串
            //ToBoolean                                 布尔值
            //ToDateTime                                时间
            //ToDouble//ToDecimal//ToSingle             小数
            //ToUInt64//ToUInt32//ToUInt16//ToByte      无符号数
            //ToInt64//ToInt32//ToInt32//ToSByte        有符号数
            //ToBase64String//ToBase64CharArray         特殊(不需要)
            object getValue = defaultValue;
            Type t = typeof(T);
            if (sourceValue != null)
            {
                getValue = sourceValue;
                if (t == typeof(string) || t == typeof(char))
                {
                    if (getValue.ToString() == string.Empty) getValue = defaultValue;
                }
                if (t == typeof(bool))
                {
                    bool temp;
                    if (bool.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                if (t == typeof(DateTime))
                {
                    DateTime temp;
                    if (DateTime.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                //  小数(可为负数)
                if (t == typeof(double) || t == typeof(decimal) || t == typeof(float))
                {
                    double temp;
                    if (double.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                //  无符号整数(不可为负数)
                if (t == typeof(ulong) || t == typeof(uint) || t == typeof(ushort) || t == typeof(byte))
                {
                    ulong temp;
                    if (ulong.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                //  有符号整数(可为负数)
                if (t == typeof(long) || t == typeof(int) || t == typeof(short) || t == typeof(sbyte))
                {
                    long temp;
                    if (long.TryParse(getValue.ToString(), out temp)) getValue = temp; 
                    else getValue = defaultValue;
                }
            }
            return (T)Convert.ChangeType(getValue, t);
        }

        /// <summary>
        /// 可为空的值类型转换 (T只能为bool,DateTime,double,decimal,float,ulong,uint,ushort,byte,long,int,short,sbyte)
        /// (string,char不需要使用Nullable类型)
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="sourceValue">源值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T? ConvertValue<T>(object sourceValue, T? defaultValue) where T : struct
        {
            //ToString//ToChar                          字符串(不需要Nullable转换)
            //ToBoolean                                 布尔值
            //ToDateTime                                时间
            //ToDouble//ToDecimal//ToSingle             小数
            //ToUInt64//ToUInt32//ToUInt16//ToByte      无符号数
            //ToInt64//ToInt32//ToInt32//ToSByte        有符号数
            //ToBase64String//ToBase64CharArray         特殊(不需要)
            object getValue = defaultValue.HasValue ? defaultValue.Value : sourceValue;
            Type t = typeof(T);
            if (sourceValue != null)
            {
                getValue = sourceValue;
                if (t == typeof(bool))
                {
                    bool temp;
                    if (bool.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                if (t == typeof(DateTime))
                {
                    DateTime temp;
                    if (DateTime.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                //  小数(可为负数)
                if (t == typeof(double) || t == typeof(decimal) || t == typeof(float))
                {
                    double temp;
                    if (double.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                //  无符号整数(不可为负数)
                if (t == typeof(ulong) || t == typeof(uint) || t == typeof(ushort) || t == typeof(byte))
                {
                    ulong temp;
                    if (ulong.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
                //  无符号整数(可为负数)
                if (t == typeof(long) || t == typeof(int) || t == typeof(short) || t == typeof(sbyte))
                {
                    long temp;
                    if (long.TryParse(getValue.ToString(), out temp)) getValue = temp;
                    else getValue = defaultValue;
                }
            }
            if (getValue == null) return (T?)getValue;
            return (T?)Convert.ChangeType(getValue, t);
        }
        #endregion

        #region GetRootDomain
        /// <summary>
        /// 获取根域名(例: user.china.com或123456.user.china.com, 返回china.com; www.china.com.cn返回china.com.cn)
        /// </summary>
        /// <param name="host">主机域名(如: user.china.com)</param>
        /// <returns>根域名</returns>
        public static string GetRootDomain(string host)
        {
            string domain = string.Empty;
            string[] domains = host.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            int len = domains.Length;
            if (len >= 2)
            {
                if (len <= 3)
                    domain = String.Format("{0}.{1}", domains[len - 2], domains[len - 1]);
                else
                {
                    List<string> roots = new List<string>(5);
                    roots.AddRange(new string[5] { "com", "net", "org", "gov", "biz" });
                    //  是否为 com.cn、net.cn、org.cn、com.tw等
                    if (roots.Contains(domains[len - 2].ToLower()))
                        domain = String.Format("{0}.{1}.{2}", domains[len - 3], domains[len - 2], domains[len - 1]);
                    else
                        domain = String.Format("{0}.{1}", domains[len - 2], domains[len - 1]);
                }
            }
            return domain;
        }
        #endregion

        #region GetKeyValueUrl 获取带替换字符的链接地址
        /// <summary>
        /// 获取带替换字符的链接地址(如: photo.aspx?page=$page$),也可直接赋值(如: photo.aspx?page=1)
        /// </summary>
        /// <param name="keyname">地址栏参数名(不区分大小写, 如: "page")</param>
        /// <param name="keyvalue">替换字符(也可直接为值,如:"$page$"或 1)</param>
        /// <returns>链接地址</returns>
        public static string GetKeyValueUrl(string keyname, string keyvalue)=> GetKeyValueUrl(HttpContext.Current.Request.RawUrl.ToLower(), keyname, keyvalue);
        public static string GetKeyValueUrl(string url, string keyname, string keyvalue)
        {
            string param = keyname.ToLower() + "=";
            string newparam = param + keyvalue;
            int flag = url.IndexOf("?");
            if (flag != -1)
            {
                string suffix = url.Substring(flag + 1);
                if (suffix.IndexOf(param) != -1)
                {
                    string oldparam = string.Empty;
                    string[] suffixArr = suffix.Split('&');
                    for (int i = 0; i < suffixArr.Length; i++)
                    {
                        if (suffixArr[i].IndexOf(param) != -1)
                            oldparam = suffixArr[i].ToString();
                    }
                    url = url.Replace(oldparam, newparam).ToString();
                }
                else
                    url += "&" + newparam;
            }
            else
                url += "?" + newparam;
            return url;
        }
        #endregion

        #region GetKeyValueFromUrl
        public static string GetKeyValueFromUrl(string url, string keyname)
        {
            string keyvalue = string.Empty;
            string param = keyname.ToLower() + "=";
            int flag = url.IndexOf("?");
            if (flag != -1)
            {
                string suffix = url.Substring(flag + 1);
                if (suffix.IndexOf(param) != -1)
                {
                    string[] suffixArr = suffix.Split('&');
                    for (int i = 0; i < suffixArr.Length; i++)
                    {
                        if (suffixArr[i].IndexOf(param) != -1)
                            keyvalue = suffixArr[i].ToString().Replace(param, string.Empty);
                    }
                }
            }
            return keyvalue;
        }
        #endregion

        #region GetSourceCode
        public static string GetSourceCode(string url)=> GetSourceCode(url, Encoding.UTF8);
        public static string GetSourceCode(string url, Encoding encoding)
        {
            string sourceCode = string.Empty;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                response = (HttpWebResponse)WebRequest.Create(url).GetResponse();
                reader = new StreamReader(response.GetResponseStream(), encoding);
                sourceCode = reader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (response != null)
                    response.Close();
            }
            return sourceCode.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }
        #endregion

        #region 防sql注入
        /// <summary>
        /// 防sql注入
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string filterSql(this string source)
        {
            source = source.Replace("'", "''");
            source = Regex.Replace(source, "delete", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "drop", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "update", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "insert", " ", RegexOptions.IgnoreCase);
            source = source.Replace("--", " ");
            source = source.Replace("\"", "“");
            source = source.Replace(";", "；");
            source = source.Replace("(", "（");
            source = source.Replace(")", "）"); 
            //去除执行存储过程的命令关键字
            source = Regex.Replace(source, "exec", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "execute", " ", RegexOptions.IgnoreCase); 
            //去除系统存储过程或扩展存储过程关键字
            source = Regex.Replace(source, "xp_", "x p_", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "sp_", "s p_", RegexOptions.IgnoreCase);
            //防止脚本注入
            source = Regex.Replace(source, "<script", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<link", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<applet", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<embed", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<object", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<form", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<frame", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<iframe", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<body", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<style", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</script", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</link", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</applet", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</embed", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</object", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</form", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</frame", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</iframe", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</body", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "</style", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<?php", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "javascript:", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "%3c", " ", RegexOptions.IgnoreCase);
            return source;
        }
        #endregion

        /// <summary>
        /// 防sql注入，获取Request["x"]值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyname"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetRequestValueNoSql<T>(string keyname, T defaultValue)=> ConvertValue<T>((HttpContext.Current.Request.Form[keyname] ??(HttpContext.Current.Request.QueryString[keyname] ?? "").filterSql()), defaultValue);

        /// <summary>
        /// 防sql注入，获取Request["x"]值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyname"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetRequestValue<T>(string keyname, T defaultValue)=> ConvertValue<T>((HttpContext.Current.Request.Form[keyname] ??(HttpContext.Current.Request.QueryString[keyname] ?? "")), defaultValue);
        /// <summary>
        /// 获取 Post/Get 过来的Base64字符串
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static string GetBase64Str(string keyname)
        {
            var data = GetRequestValue(keyname, string.Empty);
            return CheckBase64Str(data);
        }
        /// <summary>
        /// 检测并修复Base64字符串被多次解码出现的值异常
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static string CheckBase64Str(string base64Str)
        {
            if (base64Str.Contains("%"))
                base64Str = HttpUtility.UrlDecode(base64Str);
            base64Str = base64Str.Replace(" ", "+");
            var mod4 = base64Str.Length % 4;
            if (mod4 > 0) //补码
                base64Str = base64Str.PadRight(base64Str.Length + (4 - mod4), '=');
            return base64Str;
        }

        /// <summary>
        /// Request默认值为""
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static string GetRequest(string keyname)=> GetRequestValue(keyname, "");
        /// <summary>
        /// QueryString默认值为""
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static string GetQueryString(string keyname)=> GetQueryStringValue(keyname, "");
        /// <summary>
        /// Form默认值为""
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static string GetForm(string keyname)=> GetFormValue(keyname, "");
        /// <summary>
        /// 从requst中获取实体参数 参数属性不支持特殊类型比如 datetime 建议使用string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetParam<T>() where T : class, new()
        {
            T obj = new T();
            PropertyInfo[] allProperties = typeof(T).GetProperties();
            int nullpropnumber = 0;
            foreach (PropertyInfo property in allProperties)
            {
                try
                {
                    if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    {
                        string value = GetParamsValue(property.Name, "");
                        property.SetValue(obj, value, null);
                    }
                }
                catch
                { }
                finally
                {
                    if (property.GetValue(obj) is null) nullpropnumber++;
                }
            }
            if (nullpropnumber == allProperties.Length) obj = null;
            return obj;
        }
    }
}
