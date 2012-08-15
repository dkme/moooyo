using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace CBB.NetworkingHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailHelper
    {
        /// <summary> 
        /// 发送电子邮件 
        /// </summary> 
        /// <param name="MessageTo">收件人邮箱地址</param> 
        /// <param name="MessageSubject">邮件主题</param> 
        /// <param name="MessageBody">邮件内容</param> 
        /// <returns></returns> 
        public static bool SendMail (String decaddr, String Subject, String MessageBody)
        {
             String scaddr = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("EmailLoginName"); //发件人邮箱地址 

             return SendMail(scaddr, decaddr, Subject, MessageBody);
        }
        /// <summary> 
        /// 发送电子邮件 
        /// </summary> 
        /// <param name="MessageFrom">发件人邮箱地址</param> 
        /// <param name="MessageTo">收件人邮箱地址</param> 
        /// <param name="MessageSubject">邮件主题</param> 
        /// <param name="MessageBody">邮件内容</param> 
        /// <returns></returns> 
        public static bool SendMail(String srcaddr, String decaddr, String Subject, String MessageBody)
        {
            try
            {
                MailAddress MessageFrom = new MailAddress(srcaddr); //发件人邮箱地址 

                SmtpClient sc = new SmtpClient();
                sc.Host = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SmtpServer");
                sc.Port = int.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SmtpPort")); //指定发送邮件端口 
                if (CBB.ConfigurationHelper.AppSettingHelper.GetConfig("EmailLoginName") != "")
                {
                    //指定登录服务器的用户名和密码(发件人的邮箱登陆密码)
                    sc.Credentials = new System.Net.NetworkCredential(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("EmailLoginName"), CBB.ConfigurationHelper.AppSettingHelper.GetConfig("EmailPassword"));
                }

                return Send(MessageFrom, decaddr, Subject, MessageBody, sc);
            }
            catch
            {
                return false;
            }
        }
        /// <summary> 
        /// 发送电子邮件 
        /// </summary> 
        /// <param name="MessageFrom">发件人邮箱地址</param> 
        /// <param name="MessageTo">收件人邮箱地址</param> 
        /// <param name="MessageSubject">邮件主题</param> 
        /// <param name="MessageBody">邮件内容</param> 
        /// <returns></returns> 
        private static bool Send(MailAddress MessageFrom, string MessageTo, string MessageSubject, string MessageBody, SmtpClient sc)
        {
            MailMessage message = new MailMessage();

            // if (FileUpload1.PostedFile.FileName != "")
            // {
            // Attachment att = new Attachment("d://test.txt");//发送附件的内容
            //    message.Attachments.Add(att);
            // }

            message.From = MessageFrom;
            message.To.Add(MessageTo); //收件人邮箱地址可以是多个以实现群发 
            message.Subject = MessageSubject;
            message.Body = MessageBody;
            message.IsBodyHtml = true;
            //message.Attachments.Add(objMailAttachment);
            //message.IsBodyHtml = false; //是否为html格式 
            message.Priority = MailPriority.High; //发送邮件的优先等级 
            //AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(MessageBody, null, "text/html");
            //message.AlternateViews.Add(htmlBody);     

            try
            {
                sc.Send(message); //发送邮件 
            }
            catch
            {
                return false;
            }
            return true;

        }


    }
}
