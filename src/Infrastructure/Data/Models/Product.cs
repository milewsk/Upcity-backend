using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool HaveDiscount { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DiscountCode { get; set; }

        public PlaceMenuCategory PlaceMenuCategory { get; set; }
        public Guid PlaceMenuCategoryID { get; set; }
        public IList<ProductTag> ProductTags { get; set; }
    }
}
