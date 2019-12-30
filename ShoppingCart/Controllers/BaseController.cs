using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class BaseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Base
        public FileContentResult ShowProfile()
        {

            string UserId = User.Identity.GetUserId();
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var userImage = db.Users.Where(x => x.Id == UserId).Select(m => m.UserPhoto).FirstOrDefault();
            if(userImage == null)
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/default.png");
                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
            return new FileContentResult(userImage, "image/jpeg");

        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    string fullname = string.Concat(new string[] { user.FirstName });
                    ViewData.Add("FullName", fullname);
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}