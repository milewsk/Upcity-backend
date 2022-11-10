using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    public class PlaceOpeningHoursModel
    {
        public List<OpeningTime> OpeningTimes { get; set; }
    }

    public class OpeningTime
    {
        public DayOfWeek Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}
