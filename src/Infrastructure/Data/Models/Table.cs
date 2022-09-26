using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Table : EntityBase
    {
        public int ChairsAmount { get; set; }
        public int IsReserved { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
         
    }
}
