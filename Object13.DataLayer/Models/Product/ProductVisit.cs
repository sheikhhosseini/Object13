using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Product
{
    public class ProductVisit:BaseModel
    {
        #region Properties
        public long ProductId { get; set; }

        [Display(Name = "IP")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100 , ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string UserIp { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        #endregion

        #region Relations
        public Product Product { get; set; }
        #endregion
    }
}
