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
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int SeatNumber { get; set; }
    }
}
