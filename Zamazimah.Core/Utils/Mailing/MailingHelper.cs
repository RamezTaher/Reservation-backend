using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace Zamazimah.Core.Utils.Mailing
{

    public static class SMTPMailSender
    {
        public static bool SendMail(MessageModel message, MailSettings mailSettings, bool isAlternateView = false, AlternateView alternateView = null)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = mailSettings.Host;
                client.Port = mailSettings.Port;
                client.EnableSsl = mailSettings.EnableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential
                {
                    UserName = mailSettings.UserName,
                    Password = mailSettings.Password
                };
                var mail = new MailMessage
                {
                    IsBodyHtml = true,
                    From = new MailAddress(mailSettings.Sender),
                    Subject = message.Subject,
                    Body = message.Body
                };
                if (isAlternateView)
                {
                    mail.AlternateViews.Add(alternateView);
                }
                message.To.ForEach(to => mail.To.Add(to));
                if (message.CC == null)
                { // fix the cc in email that are null
                    message.CC = new List<string>();
                }
                message.CC.ForEach(cc => mail.CC.Add(cc));


                if (message.BCC == null)
                { // fix the bcc in email that are null
                    message.BCC = new List<string>();
                }
                message.BCC.ForEach(bcc => mail.Bcc.Add(bcc));

                if (message.Attachments != null)
                {
                    message.Attachments.ToList().ForEach(file => mail.Attachments.Add(new Attachment(file)));
                }
                client.Send(mail);
                return true;
            }
        }
    }


}
