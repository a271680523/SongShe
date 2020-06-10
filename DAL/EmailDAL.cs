///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-27 17:22:29
//CreateBy 		唐翔
//Content       邮箱邮件数据处理类
//////////////////////////////////////////////////////////////////
using Common;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Model;
using System.Collections.Generic;

namespace DAL
{
    /// <summary>
    /// 邮箱邮件数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class EmailDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;

        /// <summary>
        /// 发送重置密码邮箱账号ID
        /// </summary>
        public static int SendResetCodeEmailAccountId = SystemParameDAL.GetSystemParame(Keys.SystemParameId.SendResetPwdEmailAccountId)?.Value ?? 0;
        /// <summary>
        /// 发送重置密码邮箱账号
        /// </summary>
        public static EmailAccountModel SendResetCodeEmailAccount = new EmailDAL().GetEmailAccountModel(SendResetCodeEmailAccountId) ?? new EmailAccountModel();
        /// <summary>
        /// 密码重置链接有效期分钟数
        /// </summary>
        public static int ResetCodeEffectiveMinutes = SystemParameDAL.GetSystemParame(Keys.SystemParameId.ResetPwdEffectiveMinutes)?.Value ?? 0;

        /// <summary>
        /// 发送密码重置邮件
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="name">名称</param>
        /// <param name="path">重置密码链接地址</param>
        /// <param name="oldpwd">原密码(加密后)</param>
        /// <returns></returns>
        public static Tuple<bool, string> SendResetCodeMail(string email, string name, string path, string oldpwd)
        {
            try
            {
                DateTime nowTime = DateTime.Now;
                string code = RSAHelper.Encrypt(email + "," + oldpwd + "," + nowTime.ToString("yyyy-MM-dd HH:mm:ss"));
                string title = "对外汉语账号密码重置";
                StringBuilder mailContent = new StringBuilder();
                mailContent.Append($"<p>亲爱的{name}：<p/>");
                mailContent.Append($"<p>您好！你于{nowTime:yyyy-MM-dd HH:MM:ss}在<a href='http://{System.Web.HttpContext.Current.Request.Url.Authority}' target='_blank'>对外汉语网站</a>通过登陆账号申请重设密码。<p/>");
                mailContent.Append("<p>如果该重设密码请求不是您提出的，则忽略该电子邮件。不用担心，您的帐号很安全。<p/>");
                mailContent.Append($"<p>请：<a href='http://{System.Web.HttpContext.Current.Request.Url.Authority + path}?code={System.Web.HttpUtility.UrlEncode(code)}' target='_blank'>点击此处链接</a>进行密码重置。<p/>");
                mailContent.Append($"<p>该重置链接有效期{ResetCodeEffectiveMinutes}分钟，过期请重新发送<p/>");
                mailContent.Append($"<p>若无法点击，请复制此链接在浏览器打开：http://{System.Web.HttpContext.Current.Request.Url.Authority + path}?code={System.Web.HttpUtility.UrlEncode(code)}<p/>");
                return EmailHelper.SendMailBySmtp(email, name, SendResetCodeEmailAccount.UserName, SendResetCodeEmailAccount.EmailName, title, mailContent.ToString(), SendResetCodeEmailAccount.Host, SendResetCodeEmailAccount.Port, SendResetCodeEmailAccount.UserName, SendResetCodeEmailAccount.Password, true, MailPriority.Normal, false);
            }
            catch (Exception err)
            {
                return new Tuple<bool, string>(false, err.Message);
            }
        }

        /// <summary>
        /// 根据ID获取邮箱账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmailAccountModel GetEmailAccountModel(int id)
        {
            return db.EmailAccount.FirstOrDefault(d => d.Id.Equals(id));
        }

        /// <summary>
        /// 获取邮箱账号列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<EmailAccountModel> GetList()
        {
            var data = from s in db.EmailAccount
                       orderby s.Id descending
                       select s;
            return data;
        }
        /// <summary>
        /// 添加或修改邮箱账号
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="emailName">邮箱名称</param>
        /// <param name="emailAddress">邮箱地址</param>
        /// <param name="userName">邮箱账号</param>
        /// <param name="password">邮箱密码</param>
        /// <param name="smtpServer">邮箱SMTP地址</param>
        /// <returns></returns>
        public Tuple<int,string> AeModel(int id,string emailName,string emailAddress,string userName,string password,string smtpServer,int operateId)
        {
            var model = new EmailAccountModel();
            if (id > 0)
            {
                model = GetEmailAccountModel(id);
                if (model == null)
                    return new Tuple<int, string>(1, "邮箱账号不存在，请获取最新数据");
            }
            if (emailName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "邮箱名称不能为空");
            if (emailAddress.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "邮箱地址不能为空");
            if (userName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "邮箱账号不能为空");
            if (password.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "邮箱密码不能为空");
            if (smtpServer.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "邮箱Smtp地址不能为空");
            model.EmailName = emailName;
            model.EmailAddress = emailAddress;
            model.UserName = userName;
            model.Password = password;
            model.SmtpServer = smtpServer;
            model.OperateId = operateId;
            model.EditTime = DateTime.Now;
            if (id <= 0)
                model.AddTime = model.EditTime;
            db.Entry(model).State = id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return new Tuple<int, string>(0, "邮箱账号保存成功");
        }
        /// <summary>
        /// 删除邮箱账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tuple<int,string> DeleteModel(int id)
        {
            var model = GetEmailAccountModel(id);
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return new Tuple<int, string>(1, "邮箱账号删除成功");
        }
    }
}
