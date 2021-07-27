using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.Core.Services.Interfaces;
using Object13.DataLayer.Models.Product;
using Object13.DataLayer.Models.SiteUtilites;
using Object13.DataLayer.Repository;

namespace Object13.Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region Ctor
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<ProductGallery> _productGalleryRepository;
        private readonly IGenericRepository<ProductVisit> _productVisitRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<ProductVisit> productVisitRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productGalleryRepository = productGalleryRepository;
            _productVisitRepository = productVisitRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
        }
        #endregion

        #region Product
        public async Task AddProduct(Product product)
        {
            await _productRepository.AddEntity(product);
            await _productRepository.SaveChanges();
        }

        public async Task UpdateProduct(Product product)
        {
            _productRepository.UpdateEntity(product);
            await _productRepository.SaveChanges();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _productRepository?.Dispose();
            _productCategoryRepository?.Dispose();
            _productGalleryRepository?.Dispose();
            _productVisitRepository?.Dispose();
            _productSelectedCategoryRepository?.Dispose();
        }
        #endregion
    }
}
