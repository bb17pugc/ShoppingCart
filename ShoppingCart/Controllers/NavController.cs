 using ShoppingCart.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class NavController : BaseController
    {
        private readonly IProductRepository repository;
        public  NavController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: Nav
        public PartialViewResult Menu( string CurrentCatagory)
        {
            ViewBag.CurrentCatagory = Session["CurrentCatagory"];
            IEnumerable<string> Catagory = repository.Products.Select(x => x.Catagory).Distinct();

            return PartialView(Catagory);
        }

        public PartialViewResult PageTitle(string Url)
        {
            string[] UrlList = Url.Split('/');
            foreach (string data in UrlList)
            {

            }
            ViewBag.Action = Url.Substring(Url.LastIndexOf('/') + 1);
            ViewBag.Controller = Url.Split('/').Skip(0).FirstOrDefault();
            ViewBag.PageUrl = Url;
            return  PartialView();
        }
    }
}