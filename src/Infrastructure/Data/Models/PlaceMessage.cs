using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
   public class PlaceMessage : Message
    {
        public Guid PlaceID { get; set; }
        public Place Place { get; set; }
    }
}
