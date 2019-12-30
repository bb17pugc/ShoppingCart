using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class ProductSaleandUserInfoViewModel
    {
        public Product ProductInfo { get; set; }
        public List<UserinfoandProductSaledetailViewModel> UserInfoandOrderCount = new List<UserinfoandProductSaledetailViewModel>();
    }
}