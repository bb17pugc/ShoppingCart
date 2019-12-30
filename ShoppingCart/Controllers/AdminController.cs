using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {

        ApplicationDbContext db = new ApplicationDbContext();

        public int PageSize = 10;
        // GET: Admin
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult AllOrdersList(string SearchFound, string Search , int CurrentPage = 1)
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                if (Request.IsAjaxRequest())
                {
                    var SearchDataList = db.Orders.Where(m => m.Email.StartsWith(Search)).Select(m => m.Email).Distinct().ToList();
                    return Json(SearchDataList);
                }
            }

            List<UserOrdersandProfileList> DataList = new List<UserOrdersandProfileList>();

            if (SearchFound != null)
            {
                foreach (OrdersData data in db.Orders.Where(m => m.Email == SearchFound).OrderBy(m => m.ID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList())
                {
                    DataList.Add(new UserOrdersandProfileList { UsersOrdsers = data, UsersPhoto = db.Users.Where(m => m.Email == data.Email).Select(m => m.UserPhoto).SingleOrDefault() });
                }
            }
            else
            {
                foreach (OrdersData data in db.Orders.OrderBy(m => m.ID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList())
                {
                    DataList.Add(new UserOrdersandProfileList { UsersOrdsers = data, UsersPhoto = db.Users.Where(m => m.Email == data.Email).Select(m => m.UserPhoto).SingleOrDefault() });
                }
            }

            OrdersProfilePaginationViewModel model = new OrdersProfilePaginationViewModel();
            model.OrdersList = DataList;
            model.Pages = new PagingInfo
            {
                CurrentPage = CurrentPage,
                TotalItems = db.Orders.Count(),
                ItemsPerPage = PageSize
            };
            ViewBag.TotalOrders = db.Orders.Count();
            return View(model);
        }

        public ActionResult AdminDeleteOrder( int id)
        {
            if (id == 0)
            {
                HttpNotFound();
            }
            {
                OrdersData model = db.Orders.Find(id);
                if (model != null)
                {
                    db.Sales.RemoveRange(db.Sales.Where(m => m.OrderId == model.OrderId).ToList());
                    db.SaveChanges();
                    db.Orders.Remove(model);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("AllOrdersList");
        }

        public ActionResult AllUsersList(string RoleIdForAssignUsers , string RoleId , string SearchFound , string Search , int CurrentPage = 1)
        {
            if (RoleIdForAssignUsers != null)
            {
                ViewBag.RoleId = RoleIdForAssignUsers;
                ViewBag.CheckBox = true;
            }
            else
            {
                ViewBag.CheckBox = false;
            }

            if (!string.IsNullOrWhiteSpace(Search))
            {
                if (Request.IsAjaxRequest())
                {
                     var SearchDataList= db.Users.Where(m => m.Email.StartsWith(Search)).Select(m => m.Email).ToList();
                    return Json(SearchDataList);
                }
            }

            UsersPagesViewwModel model = new UsersPagesViewwModel();
            if (!string.IsNullOrEmpty(RoleId))
            {
                model.UsersInfo = db.Users.Where(m => m.Roles.Where(d=>d.RoleId == RoleId).Select(n=>n.UserId).ToList().Contains(m.Id)).OrderBy(a => a.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                model.Pages = new PagingInfo
                {
                    CurrentPage = CurrentPage,
                    TotalItems = db.Users.Where(m => m.Roles.Where(d => d.RoleId == RoleId).Select(n => n.UserId).ToList().Contains(m.Id)).OrderBy(a => a.Id).Count(),
                    ItemsPerPage = PageSize
                };
                return View(model);
            }
            else
            {
                model.UsersInfo = db.Users.Where(m => m.Email == (SearchFound != null && SearchFound != "" ? SearchFound : m.Email)).OrderBy(m => m.Id).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            }
            model.Pages = new PagingInfo
            {
                CurrentPage = CurrentPage,
                TotalItems = db.Users.Where(m => m.Email == (SearchFound != null && SearchFound != "" ? SearchFound : m.Email)).Count(),
                ItemsPerPage = PageSize
            };
            return View(model);
        }

        public ActionResult ProductSaleDetail(int ProductId)
        {
          
            ProductSaleandUserInfoViewModel model = new ProductSaleandUserInfoViewModel();
            model.ProductInfo = db.Products.Where(m => m.ID == ProductId).SingleOrDefault();
            var UserInfos = db.Sales.Where(m=>m.ProductId == ProductId).Select(m => m.UserId).Distinct().ToList();
            foreach (var user in UserInfos)
            {
                model.UserInfoandOrderCount.Add(new UserinfoandProductSaledetailViewModel { UserInfo = user, OrderCount = db.Sales.Where( m => m.UserId.Id == user.Id && m.ProductId == ProductId).Sum( m => m.Quantity) });
            }
            var OrderIdz = db.Orders.ToList();
            return View(model);
        }

        public ActionResult DeleteProfile(string UserId)
        {
            if (UserId == null)
            {

            }
            IEnumerable<SalesModel> UserSale = db.Sales.Where(m => m.UserId.Id == UserId).ToList();
            db.Sales.RemoveRange(UserSale);
            db.SaveChanges();
            ApplicationUser UserModel = new ApplicationUser();
            UserModel = db.Users.Find(UserId);
            db.Users.Remove(UserModel);
            db.SaveChanges();
            if (db.Orders.Any(m => m.UserId == UserId))
            {
                List<string> Ordersidz = new List<string>();
                Ordersidz = db.Orders.Where(m => m.UserId == UserId).Select(m => m.OrderId).ToList();
                db.Orders.RemoveRange(db.Orders.Where(m =>Ordersidz.Contains(m.OrderId)));
                db.SaveChanges();
                db.Sales.RemoveRange(db.Sales.Where(m => Ordersidz.Contains(m.OrderId)));
            }
            return View();
        }

        public ActionResult AddUsersId(string UserId)
        {
            GetUsers().AddUser(UserId);
            return Json(null);
        }
        public ActionResult RemoveUsersId(string UserId)
        {
            GetUsers().RemoveUser(UserId);
            return Json(null);
        }

        public AddUsersData GetUsers()
        {
            AddUsersData UsersObj = new AddUsersData();
            if (Session["Users"] == null)
            {
                Session["Users"] = UsersObj;
            }
            else
            {
                UsersObj = (AddUsersData)Session["Users"];
            }
            return UsersObj;
        }


        public void AssignRoleToUsers(string Role)
        {
            string OldRole;
            string NewRole;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            foreach (var data in GetUsers().GetUsersList())
            {
                ApplicationUser User = db.Users.Where(m => m.Id == data).SingleOrDefault();
                NewRole = db.Roles.Find(Role).Name;
                OldRole = db.Roles.Find(User.Roles.SingleOrDefault().RoleId).Name;
                UserManager.RemoveFromRole(data , OldRole);
                UserManager.AddToRole(data , NewRole);
                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges(); 
            }
            GetUsers().ClearList(); 
        }
    }
}