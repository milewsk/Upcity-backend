using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upcity.Domain.ModelsDto
{
    public class UserDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserDto()
        {

        }
        public UserDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
