using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class UserinfoandProductSaledetailViewModel
    {
        public ApplicationUser UserInfo { get; set; }

        public int OrderCount { get; set; }
    }
}