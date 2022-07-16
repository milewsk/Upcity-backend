using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class User 
    {
        public Guid ID { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public User()
        {

        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
