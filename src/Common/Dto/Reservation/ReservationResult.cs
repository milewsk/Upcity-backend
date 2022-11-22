using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Reservation
{
   public class ReservationResult
    {
        public Guid PlaceID { get; set; }
        public string PlaceName { get; set; }
        public int SeatsCount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
