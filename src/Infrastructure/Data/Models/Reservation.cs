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
        public PaymentStatus PaymentStatus { get; set; }
        public decimal Price { get; set; }

        //I don't want to make this a relationship
        public Nullable<Guid> PromotionID { get; set; }

        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}
