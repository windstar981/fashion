using fashion.Models;
using System.Net.Mail;
using System.Net;
using MailKit;
using fashion.Interfaces;
namespace fashion.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public void SendRegistrationEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException(nameof(emailAddress), "Email address cannot be null or empty.");
            }

            // Tạo nội dung email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_emailSettings.FromAddress);
            mail.To.Add(new MailAddress(emailAddress));
            mail.Subject = "Registration Confirmation";
            mail.Body = "Thank you for registering with us!";

            // Gửi email sử dụng SmtpClient
            SmtpClient smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }

    }
}
