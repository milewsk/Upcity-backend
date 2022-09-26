using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
   public class LoyalityProgramAccount : EntityBase
    {
        public int Points { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}
