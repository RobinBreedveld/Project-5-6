using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace login2.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("no-reply@shipped.nl");
                mail.To.Add($"{email}");
                mail.Subject = $"{subject}";
                mail.Body = $"<h1>{message}</h1>";
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("mail.shipped.nl", 587))
                {
                    smtp.Credentials = new NetworkCredential("no-reply@shipped.nl", "shipmefast");
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                }
            }
            return Task.CompletedTask;
        }
    }
}
