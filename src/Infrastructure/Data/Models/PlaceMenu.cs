using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class PlaceMenu : EntityBase
    {
        public Guid PlaceID { get; set; }
        public Place Place { get; set; }

        public ICollection<PlaceMenuCategory> PlaceMenuCategories { get; set; }
    }
}
