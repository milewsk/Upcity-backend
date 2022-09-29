using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    public class PlaceResult
    {
        public Guid PlaceID { get; set; }
        public string Image { get; set; }
        public decimal? Distance { get; set; }
        public string OpengHour { get; set; }
        public string CloseHour { get; set; }
    }
}
