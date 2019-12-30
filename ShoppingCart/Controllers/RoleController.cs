using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseController
    {

        ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Role
        public List<UsersRolesCountList> GetRoles()
        {
            List<UsersRolesCountList> List = new List<UsersRolesCountList>();
            foreach (var item in db.Roles.ToList())
            {
                List.Add(new UsersRolesCountList {

                    Role = item,
                    NoOfUsers = item.Users.Where(m => m.RoleId == item.Id).Count()


            });
            }
            
            return List;
        }

        public ActionResult DeleteRoles(string id)
        {
            string Message = null;
            IdentityRole role = db.Roles.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (db.Users.Where(m => m.Roles.Where(d => d.RoleId == id).Select(n => n.UserId).ToList().Contains(m.Id)).Count() > 0)
                {
                    Message = "You can not delete this role because it contains users";
                }
                else
                {
                    db.Roles.Remove(role);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Createrole" , "Role" , new {message = Message});
        }

        public ActionResult CreateRole(string message)
        {
            if (message != null)
            {
                ViewBag.RoleDeleteMessage = message;
                message = null;
            }
            RolesListViewModel RolesModel = new RolesListViewModel();
            RolesModel.RolesList = GetRoles();
            return View(RolesModel);
        }
        [HttpPost]
        public ActionResult CreateRole( RolesListViewModel RolesModel)
        {
            string emailRegex = "^[a-zA-Z]+$";
            Regex re = new Regex(emailRegex);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (string.IsNullOrWhiteSpace(RolesModel.UserRoles.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            else if (!re.IsMatch(RolesModel.UserRoles.Name))
            {
                    ModelState.AddModelError("Name", "Name is not valid");
            }
            else if (ModelState.IsValid && !(roleManager.RoleExists(RolesModel.UserRoles.Name)))
            {
                db.Roles.Add(RolesModel.UserRoles);
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Name", "Name exists already");
            }
            RolesModel.RolesList = GetRoles();
            return View(RolesModel);
        }
    }
}