using Microsoft.AspNetCore.Mvc;
using OllaInvoice.Api.Utility;
using OllaInvoice.Data;
using OllaInvoice.Entities;
using OllaInvoice.Entities.Dtos;
using System;

namespace OllaInvoice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISendEmail _em;

        public InvoiceController(IInvoiceRepository invoiceRepository, ISendEmail em)
        {
       
            _invoiceRepository = invoiceRepository;
            _em = em;
        }

        [HttpPost("add-invoice")]
        public IActionResult AddAnInvoice(InvoiceDto invoice)
        {
            try
            {
                Invoice newInvoice = new Invoice
                {
                    CustomerName = invoice.CustomerName,
                    Tax = invoice.Tax,
                    Discount = invoice.Discount,
                    ImageUrl = invoice.ImageUrl,
                    BusinessName = invoice.BusinessName,
                    Account = invoice.Account,
                    Items = invoice.Items,
                    Number = invoice.Number,
                    CustomerEmail = invoice.CustomerEmail
                };
                _invoiceRepository.AddInvoice(newInvoice);
                _em.SendInvoiceAsAttachmentAsync(newInvoice.CustomerEmail);
                return Ok(newInvoice);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}