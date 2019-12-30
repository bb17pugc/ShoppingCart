using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class CartIndexViewModel
    {
        public IEnumerable<Cartline> CartList { get; set; }
        public string ReturnUrl { get; set; }
        public PagingInfo PagesData { get; set; }
        public Decimal TotalBill { get; set; }
    }
}