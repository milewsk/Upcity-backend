using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class User 
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public UserDetails UserDetails { get; set; }
        public LoyalityProgramAccount LoyalityProgramAccount { get; set; }
    }
}
