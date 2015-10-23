using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace GoodMan.Tool.Framework
{
    public class EMail
    {
        private EmailContext emailItem;
        public EMail(EmailContext emailItem)
        {
            this.emailItem = emailItem;
        }
        public void Send()
        {
            try
            {
                System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
                Encoding eEncod = Encoding.GetEncoding("utf-8");
                if (String.IsNullOrEmpty(this.emailItem.DisplayName))
                {
                    this.emailItem.DisplayName = "校生活 -- www.xiao-life.com";
                }
                myEmail.From = new System.Net.Mail.MailAddress(this.emailItem.From.Trim(), this.emailItem.DisplayName, eEncod);
                myEmail.To.Add(this.emailItem.Recipient.Trim());
                myEmail.Subject = this.emailItem.Subject.Trim();
                myEmail.IsBodyHtml = true;
                myEmail.Body = this.emailItem.Body.Trim();
                myEmail.Priority = System.Net.Mail.MailPriority.Normal;
                myEmail.BodyEncoding = Encoding.GetEncoding("utf-8");
                GetAttachements(myEmail);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp." + myEmail.From.Host;
                if (smtp.Host.ToLower() == "smtp.xiao-life.com")
                {
                    smtp.Host = "smtp.exmail.qq.com";
                    smtp.Port = 25;
                }
                //if (smtp.Host.ToLower() == "smtp.gmail.com")
                //{
                //    smtp.EnableSsl = true;//SMTP服务器是否启用SSL加密
                //}
                smtp.Credentials = new System.Net.NetworkCredential(this.emailItem.CredentialUserName, this.emailItem.CredentialPassword);
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                smtp.Send(myEmail);
#if DEBUG
                LogHelper.Write(CommonLogger.System, LogLevel.Trace, "发邮件至" + this.emailItem.Recipient.Trim());
#endif
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.System, LogLevel.Error,
                       "MailForm:" + this.emailItem.From.Trim() + Environment.NewLine +
                       "MailTo:" + this.emailItem.Recipient.Trim() + Environment.NewLine +
                       e.ToString());
            }
        }

        private void GetAttachements(MailMessage mymail)
        {
            if (emailItem.attachments != null && emailItem.attachments.Any())
            {
                for (int i = 0, len = emailItem.attachments.Count; i < len; i++)
                {
                    try
                    {
                        System.Net.Mail.Attachment myAttachment = new System.Net.Mail.Attachment(
                            emailItem.attachments[i]);

                        //用smtpclient对象里attachments属性，添加上面设置好的myattachment  
                        mymail.Attachments.Add(myAttachment);  
                    }
                    catch { }
                }
            }
         
        }
    }

    public class EmailContext
    {
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 发件箱
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 收件箱地址
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// 关联显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 发件箱账号
        /// </summary>
        public string CredentialUserName { get; set; }
        /// <summary>
        /// 发件箱密码
        /// </summary>
        public string CredentialPassword { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public List<string> attachments { get; set; }
    }
}
