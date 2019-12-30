using ShoppingCart.Models;
using ShoppingCart.Abstract;
using System;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace ShoppingCart.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository repository;
        ApplicationDbContext db = new ApplicationDbContext();
        public int pageSize = 30;
        public  ProductsController( IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Products
        public ActionResult List( string SearchFound, string Currentpage , string Catagory , string Search)
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                if (Request.IsAjaxRequest())
                {
                    var SearchDataList = db.Products.Where(m => m.Name.StartsWith(Search)).Select(m => new {m.ID ,m.Name }).ToList();
                    return Json(SearchDataList);
                }
            }

            if (Catagory == null)
            {
                Catagory = (string)Session["CurrentCatagory"];
            }
            else
            { 
              Session["CurrentCatagory"] = Catagory;
            }
            int page;
            if (Currentpage == null)
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(Currentpage);
            }
            LitsPagesViewModel model = new LitsPagesViewModel()
            {
                CurrentCatagory = Catagory,
                ProductData = repository.Products.Where(x => (!string.IsNullOrWhiteSpace(SearchFound)) ? x.Name.StartsWith(SearchFound) : x.Catagory == Catagory).OrderBy(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList()
                ,
                PagesData = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = repository.Products.Where(x => x.Catagory == Catagory).Count(),
                    ItemsPerPage = pageSize,
                }
            };
            return View(model);
        }
    }
}