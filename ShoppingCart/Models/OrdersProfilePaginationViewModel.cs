using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class OrdersProfilePaginationViewModel
    {
        public PagingInfo Pages { get; set; }

        public IEnumerable<UserOrdersandProfileList> OrdersList { get; set; }
    }
}