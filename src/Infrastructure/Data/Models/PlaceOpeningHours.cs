using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
   public class PlaceOpeningHours : EntityBase
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Opens { get; set; }
        public TimeSpan Closes { get; set; }

        public Guid PlaceID { get; set; }
        public Place Place { get; set; }
    }
}
