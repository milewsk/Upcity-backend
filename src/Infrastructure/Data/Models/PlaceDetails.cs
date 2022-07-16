using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class PlaceDetails
    {
        public Guid ID { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public Place Place { get; set; }
    }
}
