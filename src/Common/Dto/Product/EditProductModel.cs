using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Product
{
    public class EditProductModel
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool HaveDiscount { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
