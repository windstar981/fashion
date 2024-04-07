using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using fashion.Models;


namespace fashion.Controllers
{
    public class EmailController : Controller
    {
        private readonly EmailSettings _emailSettings;

        public EmailController(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public string test()
        {
            return "123";

        }

        public async Task<IActionResult> SendEmail()    
        {
            try
            {
                // Tạo một đối tượng MimeMessage
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", _emailSettings.FromAddress));
                message.To.Add(new MailboxAddress("Recipient Name", "hackviet98@gmail.com"));
                message.Subject = "Test Email";

                // Tạo phần thân của email
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "This is a test email";
                message.Body = bodyBuilder.ToMessageBody();

                // Gửi email sử dụng SMTP
                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, false);
                    client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                ViewBag.Message = "Test email sent successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error sending test email: " + ex.Message;
            }

            return View();
        }
    }
}
