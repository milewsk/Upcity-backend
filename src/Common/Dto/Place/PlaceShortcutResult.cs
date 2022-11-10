using Common.Utils;
using NetTopologySuite.Geometries;
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
        public double? Distance { get; set; }
        public string OpeningHour { get; set; }
        public string CloseHour { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLiked { get; set; }
        public Coords Coords { get; set; }
    }
}
