using System.Net.Mail;

namespace fashion.Service
{
    public class MailService
    {
        public static void SendRegistrationEmail(string emailAddress)
        {
            // Cấu hình thông tin email
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587; // Cổng SMTP
            string userName = "mailcanhbao981@gmail.com";
            string password = "arfqorjbghnvxsuk";

            // Tạo nội dung email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(userName);
            mail.To.Add(new MailAddress(emailAddress));
            mail.Subject = "Registration Confirmation";
            mail.Body = "Thank you for registering with us!";

            // Gửi email sử dụng SmtpClient
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }
    }
}
