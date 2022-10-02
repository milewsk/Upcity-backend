using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class PlaceMenuCategory: EntityBase
    {
        public Guid PlaceMenuID { get; set; }
        public PlaceMenu PlaceMenu { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
