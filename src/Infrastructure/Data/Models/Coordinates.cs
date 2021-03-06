using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Coordinates
    {
        public Guid ID { get; set; }
        [Range(0,1)]
        public int IsDeleted { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
    }
}
