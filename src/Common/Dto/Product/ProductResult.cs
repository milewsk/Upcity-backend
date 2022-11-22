using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Product
{
    public class ProductResult
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Description { get; set; }

    }
}
