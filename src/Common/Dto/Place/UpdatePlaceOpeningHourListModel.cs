using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    public class GetPlaceOpeningHourListResult
    {
        public Guid PlaceID { get; set; }
        public List<OpeningTimeModel> OpeningTimes { get; set; }
    }

    public class UpdatePlaceOpeningHourListModel
    {
        public Guid PlaceID { get; set; }
        public List<OpeningTimeModel> OpeningTimes { get; set; }
    }

    public class OpeningTimeModel
    {
        public Guid ID { get; set; }
        public DayOfWeek Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}
