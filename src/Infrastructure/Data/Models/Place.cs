using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class Place
    {
        [Key]
        public Guid ID { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string Name { get; set; }

        public PlaceDetails PlaceDetails { get; set; }

    }
}
