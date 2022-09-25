using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class PlaceOpinion : EntityBase
    {
        public Guid PlaceID { get; set; }
        public Place Place { get; set; }
        public string FirstName { get; set; } 
        public int Rating { get; set; }
        public string Opinion { get; set; }
    }
}
