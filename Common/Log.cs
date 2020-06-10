using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Common
{
    public class Log
    {
        public static string BaseMapPath = "";
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="content"></param>
        public static void AddSystemLog(string content)
        {
            AddLog(content, "system");
        }
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="content"></param>
        public static void AddErrorLog(string content)
        {
            AddLog(content, "error");
        }
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="content"></param>
        public static void AddManagerLog(string content)
        {
            AddLog(content, "manager");
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="logType"></param>
        private static void AddLog(string content, string logType)
        {
            if (BaseMapPath.IsNullOrWhiteSpace() && HttpContext.Current != null)
                BaseMapPath = HttpContext.Current.Server.MapPath("~/");
            if (BaseMapPath.Last() == '\\')
                BaseMapPath = BaseMapPath.Substring(0, BaseMapPath.Length - 1);
            string path = BaseMapPath + $@"\Log\{logType}{DateTime.Now:yyyyMM}.txt";
            if (!Directory.Exists(BaseMapPath + @"\Log\")) //判断文件夹是否存在，若不存在，则创建
            {
                Directory.CreateDirectory(BaseMapPath + @"\Log\"); //创建文件夹
            }
            FileInfo f = new FileInfo(path);
            StreamWriter sw = File.Exists(path) ? f.AppendText() : f.CreateText();
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
