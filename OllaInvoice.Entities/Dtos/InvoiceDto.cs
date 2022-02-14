using System.Collections.Generic;

namespace OllaInvoice.Entities.Dtos
{
    public class InvoiceDto
    {
        public string CustomerName { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public string BusinessName { get; set; }
        public string Number { get; set; }
        public bool Account { get; set; }
        public string ImageUrl { get; set; }
        public string CustomerEmail { get; set; }
        public List<Item> Items { get; set; }

    }
}
