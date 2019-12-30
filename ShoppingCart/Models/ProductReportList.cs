using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class ProductReportList
    {
        List<ProductReportModel> ReportList = new List<ProductReportModel>();

        public void AddItem(ProductReportModel model)
        {
            ReportList.Add(model);
        }
    }
}