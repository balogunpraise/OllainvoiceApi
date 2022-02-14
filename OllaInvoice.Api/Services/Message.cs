using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace OllaInvoice.Api.Services
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFile[] Attachment { get; set; }


        public Message(IEnumerable<string> to, string subject, string content, IFormFile[] attachment)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x=> new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachment = attachment;
            
        }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }
    }
}
