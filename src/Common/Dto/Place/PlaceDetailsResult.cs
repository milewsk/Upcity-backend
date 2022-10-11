using Common.Dto.Place;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Models
{
    public class PlaceDetailsResult
    {
        //Place
        public PlaceResult PlaceResult { get; set; }
        //OpenHours
        public PlaceOpeningHoursModel PlaceOpeningHours { get; set; }
        //Menu
        public PlaceMenuResult PlaceMenuResult { get; set; }
    }
}
