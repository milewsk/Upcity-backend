using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class PlaceTag
    {
        public Guid ID { get; set; }
        public Guid PlaceID { get; set; }
        public Place Place { get; set; }
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
