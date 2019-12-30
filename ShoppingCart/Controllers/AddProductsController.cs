using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using System.IO;
using System.Web.Security;
using System.Web.Helpers;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AddProductsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private byte[] imageData;
        int PageSize = 10;

        // GET: AddProducts
        public ActionResult Index(string SearchFound, string Search , int CurrentPage = 1)
        {
            ViewBag.TotalProducts = db.Products.Count();
            if (!string.IsNullOrEmpty(Search))
            {
                var SearchDataList = db.Products.Where(m => m.Name.StartsWith(Search)).Select(m => m.Name).ToList();
                if (Request.IsAjaxRequest())
                {
                    return Json(SearchDataList);
                }
            }
            ProductsPagesAdminViewModel model = new ProductsPagesAdminViewModel();
            model.ProductList = db.Products.Where( m => m.Name == (SearchFound != null ? SearchFound : m.Name)).OrderBy(m => m.ID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            model.Pages = new PagingInfo
            {
                CurrentPage = CurrentPage,
                TotalItems = db.Products.Count(),
                ItemsPerPage = PageSize,
            };
            return View(model);
        }

        // GET: AddProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: AddProducts/Create
        public ActionResult Create(int ? ProductId)
        {
            Product product = new Product();
            if (ProductId != null)
            {
                product = db.Products.Find(ProductId);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            return View(product);
        }

        // POST: AddProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Price,Catagory")] Product product)
        {
            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["ProductPhoto"];

                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }
                product.ProductPhoto = imageData;
            }
            if (ModelState.IsValid)
            {
                if (product.ID != 0)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                db.Products.Add(product);
                db.SaveChanges();  
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: AddProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AddProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Price,Catagory")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: AddProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AddProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            db.Sales.RemoveRange(db.Sales.Where(a => a.ProductId == id).ToList());
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
