///////////////////////////////////////////////////////////////////
//CreateTime	2018年7月28日17:08:09
//CreateBy 		唐
//Content       多语言本地化筛选器处理
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Common
{
    /// <summary>
    /// 多语言本地化筛选器处理
    /// </summary>
    public class LocalizationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 执行操作方法之前触发
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string lang = string.Empty;
            if (!string.IsNullOrWhiteSpace(filterContext.RouteData.Values[Keys.LangName]?.ToString()))//从路由数据(url)里设置语言
            {
                lang = filterContext.RouteData.Values[Keys.LangName].ToString();
            }
            if (string.IsNullOrWhiteSpace(lang))
            {
                var rCookie = filterContext.HttpContext.Request.Cookies[Keys.LangName];
                if (rCookie != null)//根据cookie设置语言
                    lang = rCookie.Value;
            }
            if (string.IsNullOrWhiteSpace(lang))
                lang = ConfigurationManager.AppSettings[Keys.LangName];
            if (!string.IsNullOrWhiteSpace(lang))
            {
                try
                {
                    lang = lang.ToLower();
                    foreach (var tupLang in LangList)
                    {
                        if (tupLang.Item1.ToLower() == lang)
                        {
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(tupLang.Item2);
                            //把语言值设置到路由值里
                            filterContext.RouteData.Values[Keys.LangName] = tupLang.Item1;
                            // 把语言值设置保存进cookie
                            if (filterContext.HttpContext != null)
                            {
                                HttpCookie cookie = new HttpCookie(Keys.LangName, tupLang.Item2)
                                {
                                    Expires = DateTime.Now.AddDays(1)
                                };
                                filterContext.HttpContext.Response.SetCookie(cookie);
                            }
                            break;
                        }
                    }
                }
                catch
                {
                    // ignored
                }
            }
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// 支持的语言
        /// </summary>
        public static List<Tuple<string, string>> LangList { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("cn","zh-CN"),
            new Tuple<string, string>("en","en-US"),
            new Tuple<string, string>("tw","zh-TW"),
            new Tuple<string, string>("vn","vi-VN")
        };
    }
}