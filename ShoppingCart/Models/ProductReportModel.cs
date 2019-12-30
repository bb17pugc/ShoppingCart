using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class ProductReportModel
    {
        public string ProductName { get; set; }
        public string Catagory { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int Quantity { get; set; }
        public decimal ProductTotal { get; set; }
        public string OrderDate { get; set; }
    }
}