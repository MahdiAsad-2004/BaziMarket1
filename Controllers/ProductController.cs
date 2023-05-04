using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaziMarket1.Models;

namespace BaziMarket1.Controllers
{
    public class ProductController : Controller
    {
        Db_BaziMarket_Entities1 db_BaziMarket_ = new Db_BaziMarket_Entities1();
        public ActionResult Products(int? UserId, int Category, int Page, int? SortBy, int? AvailableGoods, int? StartPrice, int? EndPrice)
        {
            List<T_Product> products = new List<T_Product>();
            if (Category == 0)
            {
                products = db_BaziMarket_.T_Product.ToList();
            }
            else if (Category >= 1 && Category <= 5)
            {
                products = db_BaziMarket_.T_Group.Find(Category).T_Product.ToList();
                ViewBag.CategoryName = db_BaziMarket_.T_Group.Find(Category).GroupName;
            }
            else
            {
                return RedirectToAction("Error404", new { Userid = UserId });
            }
            if (SortBy != null)
            {
                if (SortBy == 1)
                {
                    products = products.OrderBy(t => t.ProductPrice).ToList();
                    ViewBag.SortName = "ارزان ترین";
                }
                if (SortBy == 2)
                {
                    products = products.OrderBy(t => t.ProductPrice).Reverse().ToList();
                    ViewBag.SortName = "گران ترین";
                }
                if (SortBy == 3)
                {
                    ViewBag.SortName = "جدید ترین";
                    products = products.OrderBy(t => t.ProductRegisterDate).Reverse().ToList();
                }
                if (SortBy == 4)
                {
                    ViewBag.SortName = "پرفروش ترین";
                    products = products.OrderBy(t => t.ProductSold).Reverse().ToList();
                }
                if (SortBy == 5)
                {
                    products = products.OrderBy(t => t.ProductDiscount).Reverse().ToList();
                    ViewBag.SortName = "بیشترین تخفیف";
                }

            }
            if (AvailableGoods != null)
            {
                products = products.Where(t => t.ProductInStockCount > 0).ToList();
            }
            if (StartPrice != null)
            {
                products = products.Where(t => t.ProductPrice > StartPrice).ToList();
            }
            if (EndPrice != null)
            {
                products = products.Where(t => t.ProductPrice < EndPrice).ToList();
            }
            ViewBag.UserId = UserId;
            ViewBag.Page = Page;
            ViewBag.Category = Category;
            ViewBag.SortBy = SortBy;
            ViewBag.AvailableGoods = AvailableGoods;
            ViewBag.StartPrice = StartPrice;
            ViewBag.EndPrice = EndPrice;
            ViewBag.PageCount = products.Count / 15 + 1;
            ViewBag.PageCount = products.Count / 15 + 1;
            products = products.Skip((Page - 1) * 15).Take(15).ToList();
            return View(products);
        }
        [HttpPost]
        public ActionResult Products(int? UserId, int Category, int? SortBy, int? AvailableGoods, int? StartPrice, int? EndPrice)
        {
            List<T_Product> products = new List<T_Product>();
            if (Category == 0)
            {
                products = db_BaziMarket_.T_Product.ToList();
            }
            else if (Category >= 1 && Category <= 5)
            {
                products = db_BaziMarket_.T_Group.Find(Category).T_Product.ToList();
                ViewBag.CategoryName = db_BaziMarket_.T_Group.Find(Category).GroupName;
            }
            if (SortBy != null)
            {
                if (SortBy == 1)
                {
                    products = products.OrderBy(t => t.ProductPrice).ToList();
                    ViewBag.SortName = "ارزان ترین";
                }
                if (SortBy == 2)
                {
                    products = products.OrderBy(t => t.ProductPrice).Reverse().ToList();
                    ViewBag.SortName = "گران ترین";
                }
                if (SortBy == 3)
                {
                    products = products.OrderBy(t => t.ProductRegisterDate).Reverse().ToList();
                    ViewBag.SortName = "جدید ترین";
                }
                if (SortBy == 4)
                {
                    products = products.OrderBy(t => t.ProductSold).Reverse().ToList();
                    ViewBag.SortName = "پرفروش ترین";
                }
                if (SortBy == 5)
                {
                    products = products.OrderBy(t => t.ProductDiscount).Reverse().ToList();
                    ViewBag.SortName = "بیشترین تخفیف";
                }

            }
            if (AvailableGoods == 1)
            {
                products = products.Where(t => t.ProductInStockCount > 0).ToList();
            }
            if (StartPrice != null)
            {
                products = products.Where(t => t.ProductPrice > StartPrice).ToList();
            }
            if (EndPrice != null)
            {
                products = products.Where(t => t.ProductPrice < EndPrice).ToList();
            }
            ViewBag.UserId = UserId;
            ViewBag.Page = 1;
            ViewBag.Category = Category;
            ViewBag.SortBy = SortBy;
            ViewBag.AvailableGoods = AvailableGoods;
            ViewBag.StartPrice = StartPrice;
            ViewBag.EndPrice = EndPrice;
            ViewBag.PageCount = products.Count / 15 + 1;
            products = products.Take(15).ToList();
            return View(products);
        }





        public ActionResult Product(int? UserId, int? ProductId)
        {
            ViewBag.UserId = UserId;
            T_Product product = db_BaziMarket_.T_Product.Find(ProductId);
            if (product == null)
            {
                return RedirectToAction("Error404", "Home", new { UserId = UserId });
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult AddComment(int? UserId, int? ProductId, string CommentText, int? rate)
        {
            if (UserId != null && ProductId != null)
            {
                if (!db_BaziMarket_.T_Product.Find(ProductId).T_Comment.Any(t => t.CommentUserId == UserId))
                {
                    T_Comment comment = new T_Comment();
                    comment.CommentText = CommentText;
                    comment.CommentRate = rate.Value;
                    comment.CommentUserId = UserId;
                    comment.CommentRegisterId = DateTime.Now;
                    comment.CommentProductId = ProductId;
                    db_BaziMarket_.T_Comment.Add(comment);
                    db_BaziMarket_.SaveChanges();
                }
                return RedirectToAction("Product", new { UserId = UserId, ProductId = ProductId });
            }
            return RedirectToAction("Error404", "Home", new { UserId = UserId });
        }

        [HttpPost]
        public ActionResult AddQuestion(int? UserId, int? ProductId, string QuestionText)
        {
            if (UserId != null && ProductId != null)
            {
                T_Question question = new T_Question();
                question.QuestionProductId = ProductId;
                question.QuestionUserId = UserId;
                question.QuestionRegisterDate = DateTime.Now;
                question.QuestionText = QuestionText;
                db_BaziMarket_.T_Question.Add(question);
                db_BaziMarket_.SaveChanges();
                return RedirectToAction("Product", new { UserId = UserId, ProductId = ProductId });
            }
            return RedirectToAction("Error404", "Home", new { UserId = UserId });
        }



        public ActionResult AddProductToCart(int? UserId, int? ProductId, int? ProductCount)
        {
            ViewBag.UserId = UserId;
            if (UserId  == null)
            {
                return Redirect("https://localhost:44398/User/Login");
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            T_Product product = db_BaziMarket_.T_Product.Find(ProductId);
            if (product == null || user == null)
            {
                return RedirectToAction("Error404", "Home", new { UserId = UserId });
            }
            T_ProductCart productCart = new T_ProductCart();
            productCart.ProductId = ProductId;
            productCart.ProductCartCount = ProductCount;
            if (user.T_Cart == null)
            {
                T_Cart cart = new T_Cart();
                cart.CartId = UserId.Value;
                cart.CartCost = 0;
                user.T_Cart = cart;
                user.T_Cart.T_ProductCart.Add(productCart);
                db_BaziMarket_.SaveChanges();
                return View("Product", product);
            }            
            if (user.T_Cart.T_ProductCart.Any(t => t.ProductId == ProductId))
            {
                productCart = user.T_Cart.T_ProductCart.Single(t => t.ProductId == ProductId);
                productCart.ProductCartCount += ProductCount;
                if (productCart.ProductCartCount > product.ProductInStockCount)
                {
                    productCart.ProductCartCount = product.ProductInStockCount;
                }
            }
            user.T_Cart.T_ProductCart.Add(productCart);
            db_BaziMarket_.SaveChanges();
            return View("Product", product);
        }



        [HttpPost]
        public ActionResult AddToWishlist(int? UserId, int? ProductId)
        {

            ViewBag.UserId = UserId;
            if (UserId == null)
            {
                return Redirect("https://localhost:44398/User/Login");
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            T_Product product = db_BaziMarket_.T_Product.Find(ProductId);
            if (user == null || product == null)
            {
                return RedirectToAction("Error404", new { UserId = UserId });
            }
            if (!user.T_Product.Any(t => t.ProductId == product.ProductId))
            {
                user.T_Product.Add(product);
                db_BaziMarket_.SaveChanges();
            }
            return View("Product", product);
        }


        [HttpPost]
        public ActionResult RemoveFromWishlist(int? UserId, int? ProductId)
        {
            ViewBag.UserId = UserId;
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            T_Product product = db_BaziMarket_.T_Product.Find(ProductId);
            if (user == null || product == null)
            {
                return RedirectToAction("Error404", new { UserId = UserId });
            }
            user.T_Product.Remove(product);
            db_BaziMarket_.SaveChanges();
            return View("Product", product);
        }







    }
}