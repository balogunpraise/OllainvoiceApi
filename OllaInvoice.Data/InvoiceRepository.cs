using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OllaInvoice.Entities;
using OllaInvoice.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OllaInvoice.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationContext _context;


       
        private readonly ILogger<InvoiceRepository> _logger;
        public InvoiceRepository(ILogger<InvoiceRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task AddInvoice(AppUser user, Invoice invoice)
        {
            user.Invoice.Add(invoice);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Invoice>> GetAllInvoiceAsync(string id)
        {
            return await _context.Invoices.Where(x=>x.AppUser.Id == id ).Include(x=>x.Items).ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoiceAsync()
        {
            return await _context.Invoices.ToListAsync();
        }



        public async Task<Invoice> GetInvoiceById(int id, string userId)
        {
            try
            {
                var invoice = await _context.Invoices.Where(x => x.Id == id && x.AppUser.Id == userId).Include(x=>x.Items).FirstOrDefaultAsync();
                return invoice;
            }
            catch(Exception ex)
            {
               _logger.LogError(ex.Message);
                return null;
            }
        }
        
        public async Task<Invoice> GetCurrentInvoice(int id)
        {
            try
            {
                var invoice = await _context.Invoices.Where(x => x.Id == id).FirstOrDefaultAsync();
                return invoice;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
