using OllaInvoice.Entities;
using System.Threading.Tasks;

namespace OllaInvoice.Data
{
    public interface IInvoiceRepository
    {
        void AddInvoice(Invoice invoice);
        Invoice GetInvoice();

    }
}
