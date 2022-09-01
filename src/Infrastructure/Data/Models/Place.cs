using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class Place
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public PlaceDetails PlaceDetails { get; set; }
        public Coordinates Coordinates { get; set; }
        public ICollection<Table> Tables { get; set; }
        public ICollection<Product> Products { get; set; }
        public IList<PlaceTag> PlaceTags { get; set; }
    }
}
