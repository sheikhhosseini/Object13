using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Account;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Orders
{
    public class Order : BaseModel
    {
        #region Properties
        public long UserId { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PaymentDate { get; set; }
        #endregion

        
        #region Relations
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
}
