using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Promotion : EntityBase
    {
        public Guid PlaceID { get; set; }
        public Place Place { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime EndTime { get; set; }
        public double Price { get; set; }
        //optional
        public int PersonCount { get; set; }

    }
}
