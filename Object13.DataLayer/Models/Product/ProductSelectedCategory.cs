using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Product
{
    public class ProductSelectedCategory:BaseModel
    {
        #region Properties
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }
        #endregion

        #region Relations
        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        #endregion
    }
}
