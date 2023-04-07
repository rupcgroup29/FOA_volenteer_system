using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace FOA_Server.Models
{
    public class EmailService
    {
        private SmtpClient smtpClient { get; set; }

        // our details which the email will sent from to the user
        const string ourMail = "cgroup29.ruppin@gmail.com";
        const string ourMailPass = "mhzqtjkxsxijqtwa\r\n";

        // details for preparing to send the email from us
        public EmailService()
        {
            smtpClient = new SmtpClient("smtp.gmail.com");

            smtpClient.EnableSsl = true; // Security
            smtpClient.Port = 587; // SMTP client to SMTP Server port. (port=25 means smtp server to smtp server)
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; // email is sent through the network
            smtpClient.Credentials = new NetworkCredential(ourMail, ourMailPass);
        }

        // create the mail's title & body
        public MailMessage createMailMessage(string mailToSend, string messageBody, string subject)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(mailToSend);
            mailMessage.From = new MailAddress(ourMail);
            mailMessage.Body = messageBody;
            mailMessage.Subject = subject;  
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }

        // send the email to the user
        public void SendEmail(MailMessage resetPasswordMessage)
        {
            try
            {
                smtpClient.Send(resetPasswordMessage);
                Trace.WriteLine("code send successfully");

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                Trace.WriteLine(ex.Message);
                throw;
            }

        }



    }
}
