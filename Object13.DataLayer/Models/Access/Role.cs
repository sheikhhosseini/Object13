using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Access
{
    public class Role : BaseModel
    {
        #region Properties
        [Display(Name = "عنوان سیسستمی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100 , ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Name { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }
        #endregion

        #region Relation
        public ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
