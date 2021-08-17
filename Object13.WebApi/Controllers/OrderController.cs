
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Common;
using Object13.Core.Utilites.Extention;


namespace Object13.WebApi.Controllers
{

    public class OrderController : MyBaseController
    {
        #region Ctor
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region Add To Order
        [HttpGet("add-order")]
        public async Task<IActionResult> AddProductToOrder(long productId , int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId =  User.GetUserId();
                await _orderService.AddProductToOrder(userId, productId, count);
                return JsonResponseStatus.Success(await _orderService.GetUserBasketDetails(userId));
            }
            return JsonResponseStatus.Error("not authorized");
        }

        #endregion

        #region Basket

        [HttpGet("basket-details")]
        public async Task<IActionResult> GetUserBasketDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                var details = await _orderService.GetUserBasketDetails(User.GetUserId());
                return JsonResponseStatus.Success(details);
            }
            return JsonResponseStatus.Error("not authorized");
        }


        [HttpGet("remove-basket-details/{id}")]
        public async Task<IActionResult> RemoveOrderDetail(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userOpenOrder = await _orderService.GetUserOpenOrder(User.GetUserId());
                var detail = _orderService.FindOrderDetail(userOpenOrder , id);
                if (detail != null)
                {
                    await _orderService.DeleteOrderDetail(detail);
                    var details = await _orderService.GetUserBasketDetails(User.GetUserId());
                    return JsonResponseStatus.Success(details);
                }
                else
                {
                    return JsonResponseStatus.Error("notfound");
                }
                
            }
            return JsonResponseStatus.Error("not authorized");
        }
        #endregion
    }
}
