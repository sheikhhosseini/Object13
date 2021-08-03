

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Object13.Core.DTOs.Products;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Common;
using Object13.DataLayer.Repository;

namespace Object13.WebApi.Controllers
{
    public class ProductsController : MyBaseController
    {
        #region Ctor
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Products
        [HttpGet("getproducts")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDto filter)
        {
            var products = await _productService.FilterProducts(filter);
            return JsonResponseStatus.Success(products);
        }
        #endregion

    }
}
