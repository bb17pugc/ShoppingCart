using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class ProductsPagesAdminViewModel
    {
        public PagingInfo Pages { get; set; }
        public IEnumerable<Product> ProductList { get; set; }

    }
}