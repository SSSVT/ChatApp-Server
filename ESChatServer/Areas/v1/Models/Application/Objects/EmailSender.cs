using System.Net;
using System.Net.Mail;

namespace ESChatServer.Areas.v1.Models.Application.Objects
{
    public class EmailSender
    {
        public void Send(EmailConfig config, string from, string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient(config.SmtpServer)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(config.Username, config.Password),
                EnableSsl = config.EnableSsl,
                Port = config.Port
            };

            MailMessage mailMessage = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body
            };            
            client.Send(mailMessage);
        }
    }
}
