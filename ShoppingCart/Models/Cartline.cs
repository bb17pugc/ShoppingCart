using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ShoppingCart.Models
{
    public class Cartline
    {
        public int ID { get; set; }
        public Product Products { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}