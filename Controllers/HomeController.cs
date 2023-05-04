using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaziMarket1.Models;

namespace BaziMarket1.Controllers
{
    public class HomeController : Controller
    {
        Db_BaziMarket_Entities1 db_BaziMarket_ = new Db_BaziMarket_Entities1();

        [HttpGet]
        public ActionResult Index(int? UserId)
        {          
            ViewBag.UserId = UserId;
            return View();
        }
        public ActionResult NavBar(int? UserId)
        {
            int? cartCount = 0;
            if (UserId != null)
            {
                T_User user = db_BaziMarket_.T_User.Find(UserId);
                if (user == null)
                {
                    return View("Error404");
                }
                ViewBag.Name = user.UserFirstName + " " +user.UserLastName;
                ViewBag.Username = user.Username;
                ViewBag.UserId = user.UserId;
                if (user.T_Cart != null)
                {
                    foreach (var item in user.T_Cart.T_ProductCart)
                    {
                        cartCount += item.ProductCartCount;
                    }
                    ViewBag.CartCount = cartCount;
                }
            }
            return PartialView();
        }


        public ActionResult Error404(int?UserId)
        {
            ViewBag.UserId = UserId;
            return View();
        }



        public ActionResult NewProducts()
        {
            int count = db_BaziMarket_.T_Product.ToList().Count;
            List<T_Product> products = db_BaziMarket_.T_Product.ToList();
            products = products.OrderBy(t => t.ProductRegisterDate).Reverse().Take(5).ToList();
            return PartialView("NewProducts",products);
        }

    }
}