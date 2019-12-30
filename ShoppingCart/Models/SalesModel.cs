using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class SalesModel
    {
        public int ID { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Catagory { get; set; }
        public int Quantity { get; set; }
        public ApplicationUser UserId { get; set; }
        public decimal ProductTotal { get; set; }
    }
}