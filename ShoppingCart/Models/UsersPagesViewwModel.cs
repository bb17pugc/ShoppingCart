using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class UsersPagesViewwModel
    {
        public IEnumerable<ApplicationUser> UsersInfo { get; set; }
        public PagingInfo Pages { get; set; }

    }
}