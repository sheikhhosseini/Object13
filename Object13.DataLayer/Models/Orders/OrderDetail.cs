using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Orders
{
    public class OrderDetail : BaseModel
    {
        #region Properties
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Count { get; set; }

        public long Price { get; set; }
        #endregion


        #region Relations
        public Order Order { get; set; }
        public Product.Product Product { get; set; }
        #endregion

    }
}
