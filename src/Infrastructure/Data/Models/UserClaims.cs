using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class UserClaim : EntityBase
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public int Value { get; set; }
    }
}
