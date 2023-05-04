using BaziMarket1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaziMarket1.Controllers
{
    public class CartController : Controller
    {
        Db_BaziMarket_Entities1 db_BaziMarket_ = new Db_BaziMarket_Entities1();
        
        public ActionResult Cart(int? UserId)
        {
            ViewBag.UserId = UserId;
            if (UserId != null)
            {
                T_Cart cart = db_BaziMarket_.T_Cart.Find(UserId);
                if (cart == null || cart.T_ProductCart == null)
                {
                    ViewBag.CartNull = 1;
                }
                return View(cart);
            }
            return RedirectToAction("Error404","Home",new {UserId = UserId });
        }



        [HttpPost]
        public ActionResult RemoveFromCart(int? UserId, int? ProductCartId)
        {
            ViewBag.UserId = UserId;
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            T_ProductCart productCart = db_BaziMarket_.T_ProductCart.Find(ProductCartId);
            T_Cart cart = user.T_Cart;
            if (user == null || cart == null || productCart == null)
            {
                return RedirectToAction("Error404", "Home", new { UserId = UserId });
            }
            if (productCart.ProductCartCount > 1)
            {
                productCart.ProductCartCount -= 1;
                db_BaziMarket_.SaveChanges();
                return View("Cart", cart);
            }
            cart.T_ProductCart.Remove(productCart);
            db_BaziMarket_.T_ProductCart.Remove(productCart);
            db_BaziMarket_.SaveChanges();
            return View("Cart",cart);
        }



        [HttpPost]
        public ActionResult ApplyDiscountCode(int? UserId, string DiscountCode)
        {
            ViewBag.UserId = UserId;
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            if (user == null)
            {
                return RedirectToAction("Error404", "Home", new { UserId = UserId });
            }
            T_Discount discount = db_BaziMarket_.T_Discount.SingleOrDefault(t => t.DiscountCode == DiscountCode);
            if (discount != null)
            {
                ViewBag.DiscountMax = discount.DiscountMaximum;
                ViewBag.DiscountPercent = discount.DiscountPercent;
            }
            else
            {
                ViewBag.WrongDiscountCode = true;
            }
            return View("Cart",user.T_Cart);
        }
    }
}