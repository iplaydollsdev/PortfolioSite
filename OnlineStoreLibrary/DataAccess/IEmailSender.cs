using System.Net.Mail;

namespace OnlineStoreLibrary.DataAccess
{
   public interface IEmailSender
   {
      Task SendEmailAsync(string email, string subject, string htmlMessage);
      Task SendEmailWithFileAsync(string email, string subject, string htmlMessage, Attachment file);
   }
}