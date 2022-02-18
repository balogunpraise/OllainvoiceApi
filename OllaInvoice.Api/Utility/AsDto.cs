using OllaInvoice.Entities;
using OllaInvoice.Entities.Dtos;
using System.Linq;

namespace OllaInvoice.Api.Utility
{
    public static class AsDto
    {
        public static InvoiceDto ReturnAsDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                Id = invoice.Id,
                BusinessName = invoice.BusinessName,
                Account = invoice.Account,
                CustomerEmail = invoice.CustomerEmail,
                CustomerName = invoice.CustomerName,
                Discount = invoice.Discount,
                Number = invoice.Number,
                Tax = invoice.Tax,
                ImageUrl = invoice.ImageUrl,
                Items = invoice.Items.Select(i => new ItemDto { Description = i.Description, PricePerUnit = i.PricePerUnit, Units = i.Units }).ToList()

            };
        }
    }
}
