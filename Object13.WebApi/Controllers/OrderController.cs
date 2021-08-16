
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
                return JsonResponseStatus.Success();
            }
            return JsonResponseStatus.Error("not authorized");
        }

        #endregion
    }
}
