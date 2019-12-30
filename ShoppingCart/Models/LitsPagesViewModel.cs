using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class LitsPagesViewModel
    {
        public IEnumerable<Product> ProductData { get; set; }
        public PagingInfo PagesData { get; set; }
        public string CurrentCatagory { get; set; }
    }
}