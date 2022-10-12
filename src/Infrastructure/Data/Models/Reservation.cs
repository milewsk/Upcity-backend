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
        public Table Table { get; set; }
        public Guid TableID { get; set; }

        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}
