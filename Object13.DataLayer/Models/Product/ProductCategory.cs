﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Product
{
    public class ProductCategory : BaseModel
    {
        #region Properties
        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150 , ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "عنوان لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string UrlTitle { get; set; }

        public long? ParentId { get; set; }
        #endregion

        #region Relations
        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }
        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        #endregion
    }
}
