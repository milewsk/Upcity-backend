using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    public class CreateOpeningHoursModelList
    {
        public Guid PlaceID { get; set; }
        public IList<CreateOpeningHoursModel> Items { get; set; }
    }

    public class CreateOpeningHoursModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}
