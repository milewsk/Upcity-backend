using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Models
{
    public class CreateUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public UserClaimsEnum ClaimValue { get; set; }
    }
}
