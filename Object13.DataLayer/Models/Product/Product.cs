using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Product
{
    public class Product:BaseModel
    {
        #region Properties
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string ProductName { get; set; }

        [Display(Name = "مشخصات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "وضعیت موجودی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsExist { get; set; }

        [Display(Name = "ویژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsSpecial { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Image { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long Price { get; set; }

        [Display(Name = "تعداد محصول موجود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Count { get; set; }
        #endregion

        #region Relations
        public ICollection<ProductGallery> ProductGaleries { get; set; }
        public ICollection<ProductVisit> ProductVisits { get; set; }
        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        #endregion
    }
}
