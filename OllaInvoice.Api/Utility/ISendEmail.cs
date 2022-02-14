using System.Threading.Tasks;

namespace OllaInvoice.Api.Utility
{
    public interface ISendEmail
    {
        Task SendInvoiceAsAttachmentAsync(string emailAddress);
        Task SendConfirmationEmailAsync(string emailAddress, string emailTitle, string emailBody);
        Task SendConfirmationEmail(string emailAddress, string emailBody);
    }
}
