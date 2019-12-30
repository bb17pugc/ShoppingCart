using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class OrderProfileInformationViewModel
    {
        public ApplicationUser UserInfo { get; set; }
        public int OrdersCount { get; set; }
        public int ProductsCount { get; set; }
        public decimal TotalPurchase { get; set; }
    }
}