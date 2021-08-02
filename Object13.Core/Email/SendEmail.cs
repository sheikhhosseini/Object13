using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Object13.Core.Email
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            var defaultEmail = "saeedmailsender@gmail.com";

            var mail = new MailMessage();

            var SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(defaultEmail, "فروشگاه انگولار");

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            // System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "test123456s");

            SmtpServer.EnableSsl = true;


            //SmtpServer.EnableSsl = true;
            //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            //SmtpServer.UseDefaultCredentials = false;

            SmtpServer.Send(mail);
        }
    }
}
