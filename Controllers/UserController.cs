using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaziMarket1.Models;
using System.Data.Entity;

namespace BaziMarket1.Controllers
{
    public class UserController : Controller
    {
        Db_BaziMarket_Entities1 db_BaziMarket_ = new Db_BaziMarket_Entities1();
        // GET: User
        public ActionResult Index()
        {
            return View(db_BaziMarket_.T_User);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Username,UserPassword,UserFirstName,UserLastName,UserAddress,ProfileImage")] BaziMarket1.Models.T_User user, string RepeatPassword, HttpPostedFileBase ProfileImage)
        {
            if (ModelState.IsValid)
            {
                if (db_BaziMarket_.T_User.Any(t => t.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "این شماره موبایل قبلا ثبت شده است");
                    return View(user);
                }
                if (RepeatPassword != user.UserPassword)
                {
                    ModelState.AddModelError("UserPassword", "رمز عبور با تکرار آن مطابقت ندارد");
                    return View(user);
                }
                string imageName = "user.png";
                if (ProfileImage != null)
                {
                    if (ProfileImage.ContentType != "image/jpeg" && ProfileImage.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ProfileImage", "فرمت تصویر اشتباه است");
                        return View(user);
                    }
                    if (ProfileImage.ContentLength > 500000)
                    {
                        ModelState.AddModelError("ProfileImage", "سایز تصویر بیشتر از 500 کیلوبایت است");
                        return View(user);
                    }
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(ProfileImage.FileName);
                    ProfileImage.SaveAs(Server.MapPath("/images/" + imageName));
                }
                user.ProfileImage = imageName;
                user.UserRoleId = 3;
                user.UserRegisterDate = DateTime.Now;
                user.UserIsActive = true;
                T_User t_ = new T_User();
                db_BaziMarket_.T_User.Add(user);
                db_BaziMarket_.SaveChanges();
                return RedirectToAction("Index");



              
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            T_User user = db_BaziMarket_.T_User.SingleOrDefault(t => t.Username == Username && t.UserPassword == Password);
            if (user != null)
            {                
                return RedirectToAction("Index", "Home", new { UserId = user.UserId });
            }
            ViewBag.Incorrect = string.Format("نام کاربری یا رمز عبور اشتباه است !");
            return View();
        }

        [HttpGet]
        public ActionResult UserPanel(int? UserId)
        {
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            ViewBag.UserId = UserId;
            if (user == null)
            {
                return RedirectToAction("Error404",new {UserId = UserId });
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult UserPanel([Bind(Include = "UserId,Username,UserFirstName,UserLastName,UserAddress,UserIsActive,UserRegisterDate,UserPassword,UserRoleId,ProfileImage")] T_User user)
        {
            ViewBag.UserId = user.UserId;
            if (ModelState.IsValid)
            {
                db_BaziMarket_.Entry(user).State = EntityState.Modified;
                db_BaziMarket_.SaveChanges();
                ViewBag.Message = string.Format($"{user.UserFirstName + " " + user.UserLastName} عزیز \\n تغییرات شما با موفقیت ثبت شد");
                return View(user);
            }
            return View(user);
        }



        [HttpPost]
        public ActionResult ChangeProfileImage(int? UserId,HttpPostedFileBase ProfileImage)
        {
            ViewBag.UserId = UserId;
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            if (user == null)
            {
                RedirectToAction("Error404", new { UserId = UserId});
            }
            bool a = true;
            bool b  = true;
            if (ProfileImage != null)
            {
                if (ProfileImage.ContentType != "image/jpeg" && ProfileImage.ContentType != "image/png")
                {
                    ViewBag.WrongFormat = 1;
                    a = false;
                }
                if (ProfileImage.ContentLength > 500000)
                {
                    ViewBag.WrongSize = 1;
                    b = false;
                }
                if (a && b)
                {
                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(ProfileImage.FileName);
                    ProfileImage.SaveAs(Server.MapPath("/images/users/" + imageName));
                    user.ProfileImage = imageName;
                    db_BaziMarket_.SaveChanges();

                }
            }                       
            return View("UserPanel",user);
        }




        [HttpGet]
        public ActionResult ChangePassword(int? UserId)
        {
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            ViewBag.UserId = UserId;
            if (user == null)
            {
                return RedirectToAction("Error404" , new {UserId = UserId });
            }
            return View(UserId);
        }
        [HttpPost]
        public ActionResult ChangePassword(int? UserId,string UserPassword,string NewPassword,string NewPasswordRepeat)
        {
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            ViewBag.UserId = UserId;
            bool a= true;
            bool b= true;
            bool c = true;
            if (user.UserPassword != UserPassword)
            {
                ViewBag.Incorrect = 1;
                a = false;
            }
            if (NewPassword != NewPasswordRepeat)
            {
                ViewBag.NotMatch = 1;                
                b = false; 
            }
            else
            {
                if (NewPassword.Length < 8 || NewPassword.Length > 20)
                {
                    ViewBag.PassLen = 1;
                    c = false;
                }
            }            
            if (a && b && c)
            {                
                db_BaziMarket_.Entry(user).Entity.UserPassword = NewPassword;
                db_BaziMarket_.SaveChanges();
                ViewBag.Message = string.Format("رمز عبور با موفقیت تغییر یافت");
                return View(UserId);
            }
            return View(UserId);
        }



        public ActionResult Wishlist(int? UserId)
        {
            ViewBag.UserId = UserId;
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            if (user == null)
            {
                return RedirectToAction("Error404", new { UserId = UserId });
            }
            return View(user);
        }



        public ActionResult RemoveFromWishlist(int? UserId , int? ProductId)
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
            return View("Wishlist",user);
        }


        public ActionResult AddProductToCart(int? UserId, int? ProductId)
        {
            ViewBag.UserId = UserId;
            T_User user = db_BaziMarket_.T_User.Find(UserId);
            T_Product product = db_BaziMarket_.T_Product.Find(ProductId);
            if (product == null || user == null)
            {
                return RedirectToAction("Error404", "Home", new { UserId = UserId });
            }
            T_ProductCart productCart = new T_ProductCart();
            if (user.T_Cart == null)
            {
                T_Cart cart = new T_Cart();
                cart.CartId = UserId.Value;
                cart.CartCost = 0;
                user.T_Cart = cart;
                user.T_Cart.T_ProductCart.Add(productCart);
                db_BaziMarket_.SaveChanges();
                return View("Wishlist", user);
            }
            productCart = user.T_Cart.T_ProductCart.SingleOrDefault(t => t.ProductId == ProductId);
            if (productCart != null)
            {
                productCart.ProductCartCount += 1;
                if (productCart.ProductCartCount > product.ProductInStockCount)
                {
                    productCart.ProductCartCount = product.ProductInStockCount;
                }
                db_BaziMarket_.SaveChanges();
                return View("Wishlist", user);
            }
            user.T_Cart.T_ProductCart.Add(productCart);
            db_BaziMarket_.SaveChanges();
            return View("Wishlist", user);
        }

    }

}