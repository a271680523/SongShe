using System;
using System.Web;
using System.Web.Mvc;

namespace Common.Extend
{
    /// <summary>
    /// JsonResult扩展
    /// </summary>
    public class JsonResultExtend : JsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// 通过从 System.Web.Mvc.ActionResult 类继承的自定义类型，启用对操作方法结果的处理。
        /// </summary>
        /// <param name="context">执行结果时所处的上下文。</param>
        /// <exception cref="ArgumentNullException">context 参数为 null。</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("不支持的GET请求");
            }
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                response.Write(Data.ToJson(DateTimeFormat));
            }
        }
    }
}
