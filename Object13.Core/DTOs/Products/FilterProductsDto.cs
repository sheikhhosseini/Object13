using System.Collections.Generic;
using Object13.Core.DTOs.Paging;
using Object13.DataLayer.Models.Product;

namespace Object13.Core.DTOs.Products
{
    public class FilterProductsDto : BasePaging
    {
        public string Title { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public List<Product> Products { get; set; }

        public List<long> Categories { get; set; }

        public FilterProductsDto SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            return this;
        }
        public FilterProductsDto SetProducts(List<Product> products)
        {
            this.Products = products;
            return this;
        }
    }
}
