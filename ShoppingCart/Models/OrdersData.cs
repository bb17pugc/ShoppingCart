using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class OrdersData
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string OrderId { get; set; }
        public DateTime CurrentDate { get; set; }
        public Decimal TotalPurchase { get; set; }
        public int ProductsCount { get; set; }
    }
}