using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class PlaceDetails
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public string Description { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
    }
}
