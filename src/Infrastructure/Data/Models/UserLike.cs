using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
   public class UserLike : EntityBase
    {
        public Guid PlaceID { get; set; }
        public Place Place { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
