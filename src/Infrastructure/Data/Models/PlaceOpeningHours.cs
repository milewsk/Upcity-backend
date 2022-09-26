using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
   public class PlaceOpeningHours : EntityBase
    {
        public byte DayOfWeek { get; set; }
        public DateTime Opens { get; set; }
        public DateTime Closes { get; set; }

        public Guid PlaceID { get; set; }
        public Place Place { get; set; }
    }
}
