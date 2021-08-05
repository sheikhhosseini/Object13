using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Object13.Core.DTOs.Paging;
using Object13.Core.DTOs.Products;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Extention;
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

        public async Task<FilterProductsDto> FilterProducts(FilterProductsDto filter)
        {
            var productsQuery = _productRepository.GetEntitiesQuery().AsQueryable();

            switch (filter.Orderby)
            {
                case ProductOrderby.PriceAsc:
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case ProductOrderby.PriceDec:
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                    break;
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(filter.Title));
            }
            productsQuery = productsQuery.Where(p => p.Price >= filter.StartPrice);

            if (filter.Categories != null && filter.Categories.Any())
            {
                productsQuery = productsQuery.SelectMany(p => p.ProductSelectedCategories.Where(f =>
                    filter.Categories.Contains(f.ProductCategoryId)).Select(t =>
                    t.Product));
            }

            if (filter.EndPrice != 0)
            {
                productsQuery = productsQuery.Where(p => p.Price <= filter.EndPrice);
            }

            


            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
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

        #region ProductCategories

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetEntitiesQuery().Where(c=>!c.IsDelete).ToListAsync();
        }
        #endregion
    }
}
