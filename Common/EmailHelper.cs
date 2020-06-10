using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common
{
    /// <summary>
    /// 邮件处理类
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件，使用SMTP
        /// </summary>
        /// <param name="toEmail">收件人邮箱</param>
        /// <param name="toName">收件人名称</param>
        /// <param name="fromEmail">发送人邮箱</param>
        /// <param name="fromName">发送人名称</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="sHost">邮箱SMTP地址</param>
        /// <param name="sPort">SMTP端口号</param>
        /// <param name="loginName">邮箱账号</param>
        /// <param name="pwd">邮箱密码</param>
        /// <param name="isHtml">邮件内容是否是HTML</param>
        /// <param name="priority">邮件优先级</param>
        /// <param name="isAsync">是否异步发送</param>
        /// <returns></returns>
        public static Tuple<bool, string> SendMailBySmtp(string toEmail, string toName, string fromEmail, string fromName, string title, string body, string sHost, int sPort, string loginName, string pwd, bool isHtml, MailPriority priority, bool isAsync = false)
        {
            //初始化邮件数据
            MailMessage msg = new MailMessage();
            //收件人
            msg.To.Add(new MailAddress(toEmail, toName, Encoding.UTF8));
            //////抄送人
            ////msg.CC.Add(new MailAddress("133144954@qq.com", "糖3", Encoding.UTF8));
            //发件人
            msg.From = new MailAddress(fromEmail, fromName, Encoding.UTF8);
            msg.IsBodyHtml = isHtml;//是否是HTML邮件
            msg.Subject = title;//邮件标题    
            msg.SubjectEncoding = Encoding.Default;//邮件标题编码
            msg.Body = body;//邮件内容    
            msg.BodyEncoding = Encoding.Default;//邮件内容编码
            msg.Priority = priority;//邮件优先级    
            //初始化SMTP数据
            SmtpClient client = new SmtpClient
            {
                //UseDefaultCredentials = true,//设定该值将会重置Credentials数据
                Credentials = new NetworkCredential(loginName, pwd),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = sHost,
                Port = sPort
            };
            object obj = client;
            try
            {
                if (isAsync)
                    client.SendAsync(msg, obj);
                else
                    client.Send(msg);
                return new Tuple<bool, string>(true, "SUCCESS");
            }
            catch (Exception ex)
            {
                NLogger.ErrorLog(ex);
                return new Tuple<bool, string>(false, ex.Message);
            }
        }
    }
}
