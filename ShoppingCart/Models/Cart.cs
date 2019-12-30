using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace ShoppingCart.Models
{
    public class Cart
    {
        private List<Cartline> lineCollection = new List<Cartline>();

        public void AddItem( Product product  , int Quantity , string CustomerUserId)
        {   
            Cartline line = lineCollection.Where(p => p.Products.ID == product.ID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new Cartline { Products = product, Quantity = Quantity  , UserId = CustomerUserId});
            }
            else
            {
                line.Quantity += Quantity;
            }

        }
        public void RemoveItem(Product product)
        {
            lineCollection.RemoveAll(p => p.Products.ID == product.ID);
        }
        public decimal CompleteTotalValue()
        {
            return  lineCollection.Sum(p => p.Products.Price*p.Quantity);
        }
        public IEnumerable<Cartline> Lines
        {
           get { return lineCollection; }
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
        public int TotalItems()
        {
             return  lineCollection.Sum(x => x.Quantity);
        }
    }
}