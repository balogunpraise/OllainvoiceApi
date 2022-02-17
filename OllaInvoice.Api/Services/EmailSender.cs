using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OllaInvoice.Api.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly  EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
       

        public async Task SendMailAttachmentsAsync(Message message)
        {
            var mailMessage = CreateEmailWithAttachment(message);
            await SendAsync(mailMessage);
        }

       
        private MimeMessage CreateEmailWithAttachment(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<p>{0}</p>", message.Content) };

            if(message.Attachment != null && message.Attachment.Any())
            {
                byte[] fileBytes;
                foreach(var attachment in message.Attachment)
                {
                    using(var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    };
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse("application/pdf"));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }


        
        private async Task SendAsync(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);
                await client.SendAsync(message);
            }
            catch
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
