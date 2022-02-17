using System.ComponentModel.DataAnnotations;

namespace OllaInvoice.Entities.Dtos
{
    public class ItemDto
    {
        [Key]
        public string Description { get; set; }
        public int Units { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalCost => Units * PricePerUnit;
    }
}
