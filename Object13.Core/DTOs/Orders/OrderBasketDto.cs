using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object13.Core.DTOs.Orders
{
    public class OrderBasketDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
    }
}
