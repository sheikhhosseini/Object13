using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.Core.DTOs.Products;
using Object13.DataLayer.Models.Product;

namespace Object13.Core.Services.Interfaces
{
    public interface IProductService :IDisposable
    {
        #region Product
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<FilterProductsDto> FilterProducts(FilterProductsDto filter);

        #endregion
    }
}
