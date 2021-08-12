using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Account;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Product
{
    public class ProductComment : BaseModel
    {
        #region Properties
        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150 , ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string CommentText { get; set; }

        public long UserId { get; set; }

        public bool IsAccepted { get; set; }

        public long ProductId { get; set; }
        #endregion

        #region Relation

        public Product Product { get; set; }
        public User User { get; set; }
        #endregion
    }
}
