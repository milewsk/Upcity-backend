using Common.Dto.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    public class PlaceMenuResult
    {
        public List<PlaceCategoryResult> PlaceCategoryResults { get; set; }
    }

    public class PlaceCategoryResult
    {
        public string Name { get; set; }
        public List<ProductResult> ProductResults { get; set; }
    }
}
