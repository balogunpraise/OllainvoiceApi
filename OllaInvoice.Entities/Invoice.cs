using System;
using System.Collections.Generic;
using System.Linq;

namespace OllaInvoice.Entities
{
    public class Invoice 
    {
        public string Number { get; set; }
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddMonths(1);
        public virtual List<Item> Items { get; set; } = new List<Item>();
        public double Tax { get; set; } 
        public double Discount { get; set; }
        public string ImageUrl { get; set; }


        public double SubTotal => Items.Sum(i => i.TotalCost);

        public double CalculatedFees()
        {
            double calculatedFees ;
            double taxFees;
            calculatedFees = SubTotal - (SubTotal * (Discount / 100));
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
            var discount = SubTotal * (Discount / 100);
            return discount;
        }
        public double AmountDue => CalculatedFees();
        public double CalculatedWithDiscount => CalculateDiscount();
        public double TaxFees => CalculateTax();
        public bool Account { get; set; }
        public string CustomerEmail { get; set; }
    }


}

