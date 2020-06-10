using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NLog;

namespace Common
{
    /// <summary>
    /// NLog日志记录类
    /// </summary>
    public class NLogger
    {
        private static readonly Logger ClassLogger = LogManager.GetCurrentClassLogger();

        #region 记录日志基方法
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logobj"></param>
        /// <param name="loglevel"></param>
        private static void WriteLogBase(LogInfoModel logobj, LogLevel loglevel)
        {
            LogEventInfo ei = new LogEventInfo {Level = loglevel};
            ei.Properties["Request"] = $"Action:{HttpContext.Current?.Request.Url.AbsoluteUri},Params:{GetParam()}" + $",IP：{ StaticCommon.GetRealIp}";
            ei.Properties["Route"] = HttpContext.Current?.Request.RequestContext.RouteData.Values.ToJson();

            ei.Properties["Result"] = logobj.Result;
            ei.Properties["Message"] = logobj.Message;
            ei.Properties["LogType"] = loglevel.Name;
            ClassLogger.Log(ei);
        }
        #endregion

        #region debug 日志
        /// <summary>
        /// 记录Debug日志
        /// </summary>
        /// <param name="result">返回信息</param>
        public static void Debug(string result)
        {
            Debug(result, string.Empty);
        }
        /// <summary>
        /// 记录Debug日志
        /// </summary>
        /// <param name="result">返回信息</param>
        /// <param name="message">描述</param>
        public static void Debug(string result, string message)
        {
            WriteLogBase(new LogInfoModel { Result = result, Message = message }, LogLevel.Debug);
        }
        #endregion

        #region 记录日志
        /// <summary>
        /// 记录Info日志
        /// </summary>
        /// <param name="result">返回信息</param>
        public static void WriteLog(string result)
        {
            WriteLog(result, string.Empty);
        }
        /// <summary>
        /// 记录Info日志
        /// </summary>
        /// <param name="result">返回信息</param>
        /// <param name="message">描述</param>
        public static void WriteLog(string result, string message)
        {
            WriteLogBase(new LogInfoModel { Result = result, Message = message }, LogLevel.Info);
        }
        #endregion

        #region 错误日志
        /// <summary>
        /// 记录Error日志
        /// </summary>
        /// <param name="ex">错误信息</param>
        public static void ErrorLog(Exception ex)
        {
            ErrorLog(ex, string.Empty);
        }
        /// <summary>
        /// 记录Error日志
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="result">返回信息</param>
        public static void ErrorLog(Exception ex, string result)
        {
            WriteLogBase(new LogInfoModel() { Result = result, Message = ex.Message + ex.StackTrace }, LogLevel.Error);
        }
        #endregion
        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <returns></returns>
        private static string GetParam()
        {
            var result = new Dictionary<string, object>();
            var request = HttpContext.Current?.Request;
            if (request == null)
            {
                return string.Empty;
            }
            foreach (string key in request.Form.Keys)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    try
                    {
                        result.Add(key, request.Form[key]);
                    }
                    catch
                    {
                        result.Add(key, "(form)获取参数值出错！");
                    }
                }
            }
            foreach (string key in request.QueryString.Keys)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    try
                    {
                        if (result.ContainsKey(key))
                            result.Add(key + "(QueryString_" + result.Keys.Count(item => item.Contains(key)) + ")", request.QueryString[key]);
                        else result.Add(key, request.QueryString[key]);
                    }
                    catch
                    {
                        result.Add(key, "(QueryString)获取参数值出错！");
                    }
                }
            }
            return JsonConvert.SerializeObject(result);
        }
    }
    /// <summary>
    /// 日志记录实体
    /// </summary>
    public class LogInfoModel
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Result;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message;
    }
}