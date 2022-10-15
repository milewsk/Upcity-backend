using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Product
{
    public class ProductSetDiscountModel
    {
        public Guid ProductID { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
