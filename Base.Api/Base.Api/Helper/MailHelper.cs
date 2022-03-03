using System;
using System.Net.Mail;
using sg.com.titansoft;
using sg.com.titansoft.TiUtil.Debug;

namespace Base.Api.Helper
{
    public class MailHelper
    {
        public static void SendMail(string title, string body, string from, string to, bool? isHtml=false)
        {
	        var msgMail = new MailMessage {From = new MailAddress(from)};
	        var mailList = to.Split(';');
            foreach (var m in mailList)
            {
                msgMail.To.Add(m);
            }
            msgMail.Subject = title;
            if (isHtml != null) msgMail.IsBodyHtml = (bool) isHtml;
            msgMail.Body = body;
            try
            {
                Smtp.Send(msgMail);
            }
            catch (Exception e)
            {
                TiDebugHelper.Error($"Mail Send Fail: Title=>{msgMail.Subject}");
	            throw;
            }
           
        }
        private static SmtpClient smtp;
        public static SmtpClient Smtp
        {
            get
            {
                if (smtp == null)
                {
                    try
                    {
                        smtp = new SmtpClient(TiApplicationManager.GetGlobalSetting("SMTPServer"));
                        smtp.Credentials =
                            new System.Net.NetworkCredential(string.Empty, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        TiDebugHelper.Error("MailHelper " + ex.Message);
                    }
                }
                return smtp;
            }
        }
    }
}
