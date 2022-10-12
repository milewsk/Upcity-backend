﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Reservation
{
   public class ReservationResult
    {
        public Guid PlaceID { get; set; }
        public string PlaceName { get; set; }
        public int SeatsCount { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


    }
}
