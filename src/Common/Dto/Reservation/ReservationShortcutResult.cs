using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Reservation
{
    public class ReservationShortcutResult
    {
        public Guid ReservationID { get; set; }
        public string PlaceName { get; set; }
        public string ReservationDate { get; set; }
        public string  StartTime { get; set; }
        public int SeatNumber { get; set;  }
        public ReservationStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal Price { get; set; }
    }
}
