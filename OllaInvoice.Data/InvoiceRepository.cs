using Microsoft.Extensions.Logging;
using OllaInvoice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OllaInvoice.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        static List<Invoice> Invoices = new List<Invoice>();

       
        private readonly ILogger<InvoiceRepository> _logger;
        public InvoiceRepository(ILogger<InvoiceRepository> logger)
        {
            _logger = logger;
        }


        public void AddInvoice(Invoice invoice)
        {
            Invoices.Add(invoice);
        }


        public Invoice GetInvoice()
        {
            try
            {
                var invoice = Invoices.FirstOrDefault();
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
