using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Reservation
    {
        public Guid ID { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
        public Table Table { get; set; }
        public Guid TableID { get; set; }

    }
}
