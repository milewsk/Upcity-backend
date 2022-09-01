using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class Product
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool HaveDiscount { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DiscountCode { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
        public IList<ProductTag> ProductTags { get; set; }
    }
}
