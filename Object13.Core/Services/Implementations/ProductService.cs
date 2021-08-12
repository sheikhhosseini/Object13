using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IGenericRepository<ProductComment> _productCommentRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<ProductVisit> productVisitRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository, IGenericRepository<ProductComment> productCommentRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productGalleryRepository = productGalleryRepository;
            _productVisitRepository = productVisitRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _productCommentRepository = productCommentRepository;
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
            

            if (filter.Categories != null && filter.Categories.Any())
            {
                productsQuery = productsQuery.SelectMany(p => p.ProductSelectedCategories.Where(f =>
                    filter.Categories.Contains(f.ProductCategoryId)).Select(t =>
                    t.Product));
            }

            productsQuery = productsQuery.Where(p => p.Price >= filter.StartPrice);

            if (filter.EndPrice != 0)
            {
                productsQuery = productsQuery.Where(p => p.Price <= filter.EndPrice);
            }

            


            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
        }

        public async Task<Product> GetProductById(long productId)
        {
            return await _productRepository.GetEntityById(productId);
        }

        public async Task<List<Product>> GetRelatedProducts(long productId)
        {
            var product = await _productRepository.GetEntityById(productId);
            if (product == null)
            {
                return null;
            }

            var productCategoriesList = await _productSelectedCategoryRepository.GetEntitiesQuery()
                .Where(s => s.ProductId == productId).Select(f => f.ProductCategoryId).ToListAsync(); ;

            var relatedProducts = await _productRepository
                .GetEntitiesQuery()
                .SelectMany(s => s.ProductSelectedCategories.Where(f => productCategoriesList.Contains(f.ProductCategoryId)).Select(t => t.Product))
                .Where(s => s.Id != productId)
                .OrderByDescending(s => s.CreateDate)
                .Take(3).ToListAsync();

            return relatedProducts;
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
            _productCommentRepository?.Dispose();
        }
        #endregion

        #region ProductCategories
        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetEntitiesQuery().Where(c=>!c.IsDelete).ToListAsync();
        }
        #endregion

        #region ProductGallery
        public async Task<List<ProductGallery>> GetProductActiveGslleries(long productId)
        {
            return await _productGalleryRepository.GetEntitiesQuery()
                .Where(p => p.ProductId == productId && !p.IsDelete)
                .Select(p => new ProductGallery()
                {
                    ProductId = p.ProductId,
                    Id = p.Id,
                    Image = p.Image,
                    CreateDate = p.CreateDate
                })
                .ToListAsync();
        }
        #endregion

        #region ProductGallery

        public async Task AddCommentToProduct(ProductComment comment)
        {
            await _productCommentRepository.AddEntity(comment);
            await _productCommentRepository.SaveChanges();
        }

        public async Task<List<ProductCommentDto>> GetActiveProductComments(long productId)
        {
           return await _productCommentRepository.GetEntitiesQuery()
                .Include(c=>c.User)
                .Where(c => c.ProductId == productId && !c.IsDelete)
                .OrderByDescending(c=>c.CreateDate)
                .Select(c=> new ProductCommentDto
                {
                    ProductId = c.ProductId,
                    IsAccepted = c.IsAccepted,
                    CommentText = c.CommentText,
                    UserId = c.UserId,
                    UserFullName = c.User.FirstName + ' ' + c.User.LastName,
                    CreateDate = c.CreateDate.ToString("yy-MM-dd -- HH:mm")
                })
                .ToListAsync();
        }

        #endregion
    }
}
