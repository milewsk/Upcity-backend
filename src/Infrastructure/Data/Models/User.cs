﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserClaim UserClaim { get; set; }
        public UserDetails UserDetails { get; set; }
        public LoyalityProgramAccount LoyalityProgramAccount { get; set; }
        public ICollection<UserLike> UserLikes { get; set; }
        public ICollection<PrivateMessage> PrivateMessages { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
