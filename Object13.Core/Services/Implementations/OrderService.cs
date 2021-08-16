﻿
using System;
using System.Collections.Generic;
using System.Linq;
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
                .SingleOrDefaultAsync(o => 
                    o.UserId == userId && !o.IsPay && !o.IsDelete);
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
                var order = await GetUserOpenOrder(userId);

                if (count < 1)
                {
                    count = 1;
                }

                var details = await GetOrderDetails(order.Id);
                var existDetail = details.SingleOrDefault(d => d.ProductId == productId);
                if (existDetail != null)
                {
                    existDetail.Count += count;
                    _orderDetailRepository.UpdateEntity(existDetail);
                }
                else
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = productId,
                        Count = count,
                        Price = product.Price
                    };

                    await _orderDetailRepository.AddEntity(orderDetail);
                }
                await _orderDetailRepository.SaveChanges();
            }
        }

        public async Task<List<OrderDetail>> GetOrderDetails(long orderId)
        {
            return await _orderDetailRepository.GetEntitiesQuery()
                .Where(od => od.OrderId == orderId)
                .ToListAsync();
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            _orderDetailRepository?.Dispose();
            _ordeRepository?.Dispose();
        }

        #endregion
    }
}
