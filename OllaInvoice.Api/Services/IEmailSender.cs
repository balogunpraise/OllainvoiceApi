using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OllaInvoice.Api.Services
{
    public interface IEmailSender
    {
        Task SendMailAttachmentsAsync(Message message);

    }
}
