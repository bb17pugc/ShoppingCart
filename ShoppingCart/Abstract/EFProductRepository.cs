using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Abstract
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<Product> Products
        {
            get
            {
                return db.Products;
            }
        }
    }
}
