using Microsoft.AspNet.Identity;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        CountriesList Countrynames = new CountriesList();
        private byte[] imageData;

        // GET: Profile
        public ActionResult UserProfile(string UserId)
        {
            OrderProfileInformationViewModel model = new OrderProfileInformationViewModel();
            if (UserId == null)
            {
                UserId = User.Identity.GetUserId();
            }
            model.UserInfo = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
            if (db.Orders.Any(m => m.UserId == UserId))
            {
            model.OrdersCount = db.Orders.Where(m => m.UserId == UserId).Count();
            model.ProductsCount = db.Orders.Where(m => m.UserId == UserId).Sum(m => m.ProductsCount);
            model.TotalPurchase = db.Orders.Where(m => m.UserId == UserId).Sum(m => m.TotalPurchase);
            }
            return View(model);
        }

        public ActionResult EditProfile(string UserId )
        {
            ApplicationUser UserModel = new ApplicationUser();
            if (UserId != null)
            {
                UserModel = db.Users.Find(UserId);
            }
            ViewData["Countries"] = Countrynames.Countries();

            return View(UserModel);
        }
        [HttpPost]
        public ActionResult EditProfile(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserUpload"];
                    ModelState.AddModelError("UserPhoto", "Picture is required");

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }
                model.UserPhoto = imageData;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewData["Countries"] = Countrynames.Countries();
            return RedirectToAction("UserProfile");
        }
    }
}