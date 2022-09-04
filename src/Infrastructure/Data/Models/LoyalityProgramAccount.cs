using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
   public class LoyalityProgramAccount
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public int Points { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}
