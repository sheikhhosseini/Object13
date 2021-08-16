
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Object13.Core.Services.Interfaces;
using Object13.DataLayer.Models.Orders;
using Object13.DataLayer.Repository;

namespace Object13.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region Ctor
        private readonly IGenericRepository<Order> _ordeRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IUserService _userRepository;
        private readonly IProductService _productRepository;

        public OrderService(IGenericRepository<Order> ordeRepository, IGenericRepository<OrderDetail> orderDetailRepository, IUserService userRepository, IProductService productRepository)
        {
            _ordeRepository = ordeRepository;
            _orderDetailRepository = orderDetailRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region Order
        public async Task<Order> CreateUserOrder(long userId)
        {
            var order = new Order
            {
                UserId = userId
            };
            await _ordeRepository.AddEntity(order);
            await _ordeRepository.SaveChanges();

            return order;
        }

        public async Task<Order> GetUserOpenOrder(long userId)
        {
            var order = await _ordeRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(o => o.UserId == userId && !o.IsPay && !o.IsDelete);
            if (order == null)
            {
               order = await CreateUserOrder(userId);
            }

            return order;
        }

        #endregion

        #region OrderDetail
        public async Task AddProductToOrder(long userId, long productId, int count)
        {
            var user = await _userRepository.GetUserById(userId);
            var product = await _productRepository.GetProductByIdForOrder(productId);
            if (user != null && product != null)
            {
                var order = GetUserOpenOrder(userId);
                var orderDetail = new OrderDetail
                {
                    OrderId =  order.Id,
                    ProductId = productId,
                    Count = count,
                    Price = product.Price
                };

                await _orderDetailRepository.AddEntity(orderDetail);
                await _orderDetailRepository.SaveChanges();
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _ordeRepository?.Dispose();
            _orderDetailRepository?.Dispose();
        }

        #endregion
    }
}
