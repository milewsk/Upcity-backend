using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Reservation
{
    public class CreateReservationModel
    {
        //przesłać cenę startową wraz z place do placeSlice
        public Guid PlaceID { get; set; }
        public decimal Price { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int SeatNumber { get; set; }
    }
}
