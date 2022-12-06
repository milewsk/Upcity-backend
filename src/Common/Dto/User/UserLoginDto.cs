using Common.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto
{
   public class UserLoginDto
    {
        public string Jwt { get; set; } 
        public string FirstName { get; set; }
        public int Claim { get; set; }
        public int LoyalityPoints { get; set; }
        public Guid? OwnerPlaceID { get; set; }
    }
}
