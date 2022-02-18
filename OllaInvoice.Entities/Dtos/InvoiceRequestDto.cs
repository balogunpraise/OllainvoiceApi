using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OllaInvoice.Entities.Dtos
{
    public class InvoiceRequestDto
    {
        public string CustomerName { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public string BusinessName { get; set; }
        public bool Account { get; set; }
        public string ImageUrl { get; set; }
        public string CustomerEmail { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
