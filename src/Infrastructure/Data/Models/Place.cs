using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class Place : EntityBase
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public byte IsActive { get; set; }
        public PlaceDetails PlaceDetails { get; set; }
        public Coordinates Coordinates { get; set; }
        public ICollection<Table> Tables { get; set; }
        public ICollection<Product> Products { get; set; }
        public IList<PlaceTag> PlaceTags { get; set; }
    }
}
