using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    class PlaceResult
    {
        public Guid PlaceID { get; set; }
        public string Image { get; set; }
        public decimal? Distance { get; set; }
        public DayOfWeek OpenningHour { get; set; }

    }
}
