using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class OrdersPagesViewmodel
    {
        public IEnumerable<OrdersData> OrdersData {get;set;}

        public PagingInfo PagesData { get; set; }
    }
}