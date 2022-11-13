using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Reservation : EntityBase
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SeatNumber { get; set; }

        public ReservationStatus Status { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }

        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}
