

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
            //filter.TakeEntity = 3;
            var products = await _productService.FilterProducts(filter);
            return JsonResponseStatus.Success(products);
        }
        #endregion

        #region GetProductCategories

        [HttpGet("products-categories")]
        public async Task<IActionResult> GetProductsCategories()
        {
            return JsonResponseStatus.Success(await _productService.GetAllActiveProductCategories());
        }

        #endregion

        #region ProductDetails
        [HttpGet("products-detail/{id}")]
        public async Task<IActionResult> GetProductDetail(long id)
        {
            var product = await _productService.GetProductById(id);
            var productgalleries = await _productService.GetProductActiveGslleries(id);
            if (product != null)
            {
                return JsonResponseStatus.Success(new {product = product , galleries = productgalleries });
            }
            return JsonResponseStatus.NotFound("4545");
        }
        #endregion
    }
}
