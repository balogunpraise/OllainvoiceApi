using OllaInvoice.Entities;
using OllaInvoice.Entities.AuthEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OllaInvoice.Data
{
    public interface IInvoiceRepository
    {
        Task AddInvoice(AppUser user, Invoice invoice);
        Task<IEnumerable<Invoice>> GetAllInvoiceAsync(string id);
        Task<IEnumerable<Invoice>> GetAllInvoiceAsync();
        Task<Invoice> GetInvoiceById(int id);
        Task<Invoice> GetCurrentInvoice(int id);

    }
}
