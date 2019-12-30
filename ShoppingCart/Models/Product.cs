using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression("^[A-Z a-z]+$", ErrorMessage = "Invalid first name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [Required]
        [RegularExpression("^[a-z A-Z]+$", ErrorMessage = "Invalid first name")]
        public string Catagory { get; set; }
        public int ProductSaleCount { get; set; }
        public byte[] ProductPhoto { get; set; }
    }

}