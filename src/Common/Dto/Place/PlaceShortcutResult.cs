using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    public class PlaceShortcutResult
    {
        public Guid PlaceID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal? Distance { get; set; }
        public string OpeningHour { get; set; }
        public string CloseHour { get; set; }
    }
}
