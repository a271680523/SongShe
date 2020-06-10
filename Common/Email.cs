using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Common
{
    public class Email
    {
        /// <summary>  
        /// 发送邮件程序调用方法 SendMail("abc@126.com", "某某人", "cba@126.com", "某某人", "你好", "我测试下邮件", "邮箱登录名", "邮箱密码", "smtp.126.com", true,);  
        /// </summary>  
        /// <param name="from">发送人邮件地址</param>  
        /// <param name="fromName">发送人显示名称</param>  
        /// <param name="to">接收人邮箱地址</param>
        /// <param name="toName">接收人显示名称</param>
        /// <param name="subject">标题</param>  
        /// <param name="body">内容</param>  
        /// <param name="username">邮件登录名</param>  
        /// <param name="password">邮件密码</param>  
        /// <param name="server">邮件服务器 smtp服务器地址</param>  
        /// <param name="isHtml"> 是否是HTML格式的邮件 </param>  
        /// <returns>send ok</returns>  
        public static bool SendMail(string from, string fromName, string to, string toName, string subject, string body, string server, string username, string password, bool isHtml)
        {
            //邮件发送类  
            MailMessage mail = new MailMessage();
            try
            {
                //是谁发送的邮件  
                //mail.From = new MailAddress(from, fromName);
                mail.From = new MailAddress(username);
                //发送给谁  
                //mail.To.Add(to);
                mail.To.Add(new MailAddress(to, toName, Encoding.UTF8));
                //标题  
                mail.Subject = subject;
                //内容编码  
                mail.BodyEncoding = Encoding.Default;
                //发送优先级  
                mail.Priority = MailPriority.High;
                //邮件内容  
                mail.Body = body;
                //是否HTML形式发送  
                mail.IsBodyHtml = isHtml;
                //邮件服务器和端口  
                SmtpClient smtp = new SmtpClient()//server, 25
                {
                    //UseDefaultCredentials = true,
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Credentials = new System.Net.NetworkCredential(username, password)
                };
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Port = 25;
                smtp.Host = "smtp.qq.com"; // 如 smtp.163.com, smtp.gmail.com 
                //指定发送方式  
                //发件人身份验证,否则163   发不了  
                smtp.UseDefaultCredentials = true;
                //指定登录名和密码
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //超时时间  
                smtp.EnableSsl = false;
                smtp.Timeout = 10000;
                smtp.Send(mail);
                return true;
            }
            //catch (Exception)
            //{
            //    return false;
            //}
            finally
            {
                mail.Dispose();
            }
        }

        //读取指定URL地址的HTML，用来以后发送网页用  
        public static string ScreenScrapeHtml(string url)
        {
            //读取stream并且对于中文页面防止乱码  
            StreamReader reader = new StreamReader(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8);
            string str = reader.ReadToEnd();
            reader.Close();
            return str;
        }

        //发送plaintxt  
        public static bool SendText(string from, string fromname, string to, string toName, string subject, string body, string server, string username, string password)
        {
            return SendMail(from, fromname, to, toName, subject, body, server, username, password, false);
        }

        //发送HTML内容  
        public static bool SendHtml(string from, string fromname, string to, string toName, string subject, string body, string server, string username, string password)
        {
            return SendMail(from, fromname, to, toName, subject, body, server, username, password, true);
        }

        //发送指定网页  
        public static bool SendWebUrl(string from, string fromname, string to, string toName, string subject, string server, string username, string password, string url)
        {
            //发送制定网页  
            return SendHtml(from, fromname, to, toName, subject, ScreenScrapeHtml(url), server, username, password);

        }
        //默认发送格式  
        public static bool SendEmailDefault(string toEmail, string toName, string fUserName, string fPass, string fTimes)
        {
            StringBuilder mailContent = new StringBuilder();
            mailContent.Append("亲爱的×××会员：<br/>");
            mailContent.Append("    您好！你于");
            mailContent.Append(DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));
            mailContent.Append("通过<a href='#'>×××</a>管理中心审请找回密码。<br/>");
            mailContent.Append("　　　为了安全起见，请用户点击以下链接重设个人密码：<br/><br/>");
            string url = "http://www.×××.×××/SignIn/Rest?u=" + fUserName + "&s=" + fPass + "&t=" + fTimes;
            mailContent.Append("<a href='" + url + "'>" + url + "</a><br/><br/>");
            mailContent.Append(" (如果无法点击该URL链接地址，请将它复制并粘帖到浏览器的地址输入框，然后单击回车即可。)");
            return SendHtml(ConfigurationManager.AppSettings["EmailName"], "会员管理中心", toEmail, toName, "×××找回密码", mailContent.ToString(), ConfigurationManager.AppSettings["EmailService"], ConfigurationManager.AppSettings["EmailName"], ConfigurationManager.AppSettings["EmailPass"]); //这是从webconfig中自己配置的。 
        }

        //webconfig配置信息
        // < add key = "EmailName" value = "××××@163.com" />
        //    < add key = "EmailPass" value = "××××" />
        //       < add key = "EmailService" value = "smtp.163.com" />

        //说明： 这里面的"EmailService"得与你自己设置邮箱的smtp/POP3/...服务要相同， 大部分是根据@后面的进行配置。我是用163邮箱配置的。 可以根据自己需要自己配置。




        ////后台调用的方法
        //public ActionResult SendEmail(string EmailName)
        //           {
        //               EmailName = Helper.FI_DesTools.DesDecrypt(EmailName);
        //               if (!Regex.IsMatch(EmailName, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
        //               {
        //                   return Content("0");
        //               }
        //               string f_username = "";
        //               string f_pass = "";
        //               string f_times = Helper.FI_DesTools.DesEncrypt(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //               List<user> list = (from a in users where a.emailaddress == EmailName select a).ToList();
        //               if (list.Count > 0)
        //               {
        //                   f_username = Helper.FI_DesTools.DesEncrypt(list[0].×××);
        //                   f_pass = Helper.FI_DesTools.DesEncrypt(list[0].×××);

        //                   bool flag = Helper.MailService.SendEmailDefault(EmailName, “×××”,“×××”, “×××”);  //这里面的参数根据自己需求自己定，最好进行加密
        //                   if (flag)
        //                   {
        //                       return Content("true");
        //                   }
        //                   else
        //                   {
        //                       return Content("false");
        //                   }
        //               }
        //               else
        //               {
        //                   return Content("false");
        //               }
        //}
    }
}
