using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace OnlineStoreLibrary.DataAccess
{
   public class EmailSender : IEmailSender
   {

      private readonly string _smtpServer = "smtp.gmail.com";
      private readonly int _smtpPort = 587;
      private readonly string _smtpUsername = "devportfoliosite@gmail.com";
      private readonly string _smtpPassword = "wthqkrtgorzqiwrt";

      public async Task SendEmailAsync(string email, string subject, string htmlMessage)
      {
         try
         {
            var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
            {
               UseDefaultCredentials = false,
               Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
               EnableSsl = true
            };

            var message = new MailMessage(_smtpUsername, email, subject, htmlMessage)
            {
               IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(message);
         }
         catch
         {

         }
      }
      public async Task SendEmailWithFileAsync(string email, string subject, string htmlMessage, Attachment file)
      {
         try
         {
            var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
            {
               UseDefaultCredentials = false,
               Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
               EnableSsl = true
            };

            var message = new MailMessage(_smtpUsername, email, subject, htmlMessage)
            {
               IsBodyHtml = true
            };
            message.Attachments.Add(file);
            await smtpClient.SendMailAsync(message);
         }
         catch
         {

         }
      }
   }
}
