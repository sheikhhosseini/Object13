using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Security.Principal;
using Object13.DataLayer.Models.Access;
using Object13.DataLayer.Models.Main;
using Object13.DataLayer.Models.Orders;
using Object13.DataLayer.Models.Product;

namespace Object13.DataLayer.Models.Account
{
    public class User : BaseModel
    {
        #region Properties
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100 , ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string EmailActiveCode { get; set; }

        public bool IsActivated { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Address { get; set; }

        [Display(Name = "تصویر پروفایل")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string ImageAvatar { get; set; }

        [Display(Name = "سن")]
        public int Age { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را ثبت کنید")]
        public bool Gender { get; set; }

        [Display(Name = "متن بیو")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Bio { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string MobileNumber { get; set; }
        #endregion

        #region Relation
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<Order> Orders { get; set; }
        #endregion
    }
}
