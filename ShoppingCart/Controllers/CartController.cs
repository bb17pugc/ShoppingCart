using ShoppingCart.Abstract;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace ShoppingCart.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        private IProductRepository repository;
        private int CartPageSize = 10;
        ApplicationDbContext db = new ApplicationDbContext();
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        public ActionResult PlaceOrder(Cart Cart)
        {
            for (int i=1; i <= 12; i++)
            {
                if (Cart.Lines.Count() > 0)
                {
                    string UserId = User.Identity.GetUserId();
                    OrdersData Ordersmodel = new OrdersData();
                    Ordersmodel.UserId = User.Identity.GetUserId();
                    Ordersmodel.Email = User.Identity.GetUserName();
                    Ordersmodel.OrderId = (User.Identity.GetUserId() + DateTime.Now.AddMonths(-i)).Replace(@":", "");
                    Ordersmodel.CurrentDate = DateTime.Now.AddMonths(-i);
                    Ordersmodel.TotalPurchase = Cart.CompleteTotalValue();
                    Ordersmodel.ProductsCount = Cart.Lines.Count();
                    Ordersmodel.OrderId = Ordersmodel.OrderId.Replace(@"/", "");
                    db.Orders.Add(Ordersmodel);
                    db.SaveChanges();

                    SalesModel Sales = new SalesModel();

                    foreach (var item in Cart.Lines)
                    {
                        Sales.OrderId = Ordersmodel.OrderId;
                        Sales.Catagory = item.Products.Catagory;
                        Sales.Description = item.Products.Description;
                        Sales.ProductId = item.Products.ID;
                        Sales.Name = item.Products.Name;
                        Sales.Price = item.Products.Price;
                        Sales.Quantity = item.Quantity;
                        Sales.UserId = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
                        Sales.ProductTotal = item.Products.Price * item.Quantity;
                        db.Sales.Add(Sales);
                        db.SaveChanges();
                        Product product = db.Products.Where(m => m.ID == item.Products.ID).SingleOrDefault();
                        product.ProductSaleCount = product.ProductSaleCount + item.Quantity;
                        db.SaveChanges();
                    }
                }
            }
            Cart.Clear();
            return RedirectToAction("OrderMessage" , "Cart");
        }
        public ViewResult Index(Cart Cart , string ReturnUrl , int CurrentPage =  1)
        {
            CartIndexViewModel CartModel = new CartIndexViewModel
            {

                CartList = Cart.Lines.OrderBy(x => x.ID).Skip((CurrentPage - 1) * CartPageSize).Take(CartPageSize).ToList(),
                ReturnUrl = ReturnUrl,
                TotalBill = Cart.CompleteTotalValue(),
                PagesData = new PagingInfo
                {
                    CurrentPage = CurrentPage,
                    TotalItems = Cart.Lines.Count(),
                    ItemsPerPage = CartPageSize
                }


            };
             
            return View(CartModel);
        }
        [HttpGet]
        public JsonResult TotalItems(Cart Cart, string ReturnUrl)
        {
            return Json(Cart.TotalItems() , JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddtoCart(Cart cart ,int ID)
        {
            Product product = repository.Products.Where(x => x.ID == ID).FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product , 1 , User.Identity.GetUserId());
                var base64 = Convert.ToBase64String(product.ProductPhoto);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                return this.Json(new
                {
                    Count = cart.TotalItems(),
                    Message = product.Name + " of price " + product.Price + " is added.",
                    ImagePath = imgsrc
                });
            }
            return this.Json(new
            {
                NoItem = "The item does not exist"
            });
        }
        public RedirectToRouteResult Remove(Cart cart , int ProductId , string ReturnUrl)
        {
            Product product = repository.Products.Where(x => x.ID == ProductId).FirstOrDefault();
            if (product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        public ViewResult DisplaysOrders(int CurrentPage = 1)
        {
            string UserId = User.Identity.GetUserId();
                  OrdersPagesViewmodel Orders = new OrdersPagesViewmodel
            {

                OrdersData = db.Orders.OrderByDescending(x => x.UserId == UserId).Skip((CurrentPage - 1) * CartPageSize).Take(CartPageSize).Where(x => x.UserId == UserId).ToList(),
                PagesData = new PagingInfo
                {
                    CurrentPage = CurrentPage,
                    TotalItems = db.Orders.Where(x => x.UserId == UserId).Count(),
                    ItemsPerPage = CartPageSize
                }


            };
            return View(Orders);
        }

        public ActionResult RemoveOrder(int id)
        {
            OrdersData Ordermodel = db.Orders.Find(id);
            db.Orders.Remove(Ordermodel);
            db.SaveChanges();
            db.Sales.RemoveRange(db.Sales.Where(x => x.OrderId == Ordermodel.OrderId).ToList());
            db.SaveChanges();
            return RedirectToAction("DisplaysOrders");
        }
        public ViewResult DisplaysOrdersDetail(string id , int DeleteOrderId = 0)
        {
            string UserId = User.Identity.GetUserId();
            IEnumerable<SalesModel> OrderDetail = new List<SalesModel>();
            OrderDetail = db.Sales.Where(x => x.OrderId == id).ToList();

            if (OrderDetail.Count() != 0 && DeleteOrderId != 0)
            {
                ViewBag.DeleteMessage = "Are you sure to delete this order";
                ViewBag.DeleteOrderId = DeleteOrderId;
            }
            else
            {
                ViewBag.DeleteMessage = null;
            }
            return View(OrderDetail);
        }
        public ViewResult OrderMessage(Cart cart)
        {
            cart.Clear();
            return View();
        }
    }
}