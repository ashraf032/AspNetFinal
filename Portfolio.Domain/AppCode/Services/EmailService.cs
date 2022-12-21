using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.AppCode.Services
{
    public class EmailService
    {
        private readonly EmailServiceOptions options;

        public EmailService(IOptions<EmailServiceOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string answer)
        {
            string fromEmail = options.UserName;
            SmtpClient smtpClient = new SmtpClient(options.SmtpServer, options.smtpPort);
            smtpClient.Credentials = new NetworkCredential(fromEmail, options.Password);
            smtpClient.EnableSsl = true;

            MailAddress from = new MailAddress(fromEmail, options.DisplayName);
            MailAddress to = new MailAddress(toEmail);

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = options.Subject;
            mailMessage.Body = "Müracietiniz üçün təşəkkürlər,<br/>Müraciətinizin cavabı:" +
                    $"<span>{answer}</span>";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }
    }
    public class EmailServiceOptions
    {
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string SmtpServer { get; set; }
        public int smtpPort { get; set; }
        public string UserName { get; set; }
        public string AccountPassword { get; set; }
        public string Password { get; set; }
    }
}
