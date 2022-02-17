using OllaInvoice.Entities.AuthEntities;
using OllaInvoice.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OllaInvoice.Entities
{
    public class Invoice : BaseEntity
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddMonths(1);
        public virtual IList<Item> Items { get; set; } = new List<Item>();
        public AppUser AppUser { get; set; } //newly added
        public double Tax { get; set; } 
        public double Discount { get; set; }
        public string ImageUrl { get; set; }


        public double SubTotal => Items.Sum(i => i.TotalCost);

        public double CalculatedFees()
        {
            double calculatedFees ;
            double taxFees;
            calculatedFees = SubTotal - Discount;
            taxFees = SubTotal * Tax / 100;
            return calculatedFees + taxFees;
        }

        public double CalculateTax()
        {
            var tax = SubTotal * (Tax / 100);
            return tax;
        }
        public double CalculateDiscount()
        {
            var discount = Discount;
            return discount;
        }
        public double AmountDue => CalculatedFees();
        public double CalculatedWithDiscount => CalculateDiscount();
        public double TaxFees => CalculateTax();
        public bool Account { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
    }


}

