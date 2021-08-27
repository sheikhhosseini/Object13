using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Object13.DataLayer.Models.Main;

namespace Object13.Core.DTOs.Account
{
    public class UserProfileInfoDto
    {
        #region Properties

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Email { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string LastName { get; set; }


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
    }
}
