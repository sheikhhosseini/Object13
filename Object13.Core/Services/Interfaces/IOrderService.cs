using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Orders;

namespace Object13.Core.Services.Interfaces
{
    interface IOrderService : IDisposable
    {
        #region Order
        Task<Order> CreateUserOrder(long userId);
        Task<Order> GetUserOpenOrder(long userId);
        #endregion

        #region OrderDetail

        Task AddProductToOrder(long userId , long productId , int count);

        #endregion
    }
}
