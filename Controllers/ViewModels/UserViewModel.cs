using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaziMarket1.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [RegularExpression("(09)[0-9]{9}",ErrorMessage = "فرمت {0} اشتباه است")]
        public string Username { get; set; }
        [Display(Name = "رمز عبور")]
        [DataType("password")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "رمز عبور باید حداقل 8 و حداکثر 20 کاراکتر باشد")]
        public string UserPassword { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [RegularExpression("[\\u0600-\\u06FF\\s]+$",ErrorMessage = " {0} باید فارسی باشد ")]
        public string UserFirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [RegularExpression("[\\u0600-\\u06FF\\s]+$", ErrorMessage = " {0} باید فارسی باشد ")]
        public string UserLastName { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public System.DateTime UserRegisterDate { get; set; }
        [Display(Name = "وضعیت")]
        public bool UserIsActive { get; set; }
        [Display(Name = "تصویر پروفایل")]
        [DataType("file")]
        public string ProfileImage { get; set; }
        [Display(Name = "آدرس")]
        public string UserAddress { get; set; }
        [Display(Name = "نقش")]
        public byte UserRoleId { get; set; }

    }



    [MetadataType(typeof(UserViewModel))]   
    public partial class T_User
    {

    }

}