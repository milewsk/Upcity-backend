using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upcity.Database;
using upcity.Domain.Models;
using upcity.Domain.ModelsDto;

namespace upcity.Data.UserRepo
{
    public class UserRepository : IUserRepository2
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserByGuid(Guid guid) {
            var user = (from x in _context.Users where guid == x.Id select x).FirstOrDefault();

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = (from x in _context.Users where email == x.Email select x).FirstOrDefault();

            return user;
        }

        public User CreateUser(UserDto userDto)
        {
            //logic for validation 
            if (GetUserByEmail(userDto.Email) == null)
            {
                User user = new User(userDto.Email, BCrypt.Net.BCrypt.HashPassword(userDto.Password));
                _context.Users.Add(user);
                _context.SaveChanges();

                user = GetUserByEmail(user.Email);

                return user;
            }

            return null;
        }

        public User LoginUser(UserDto userDto)
        {
            if (true)
            {
                var user = GetUserByEmail(userDto.Email);

                if (user == null) return null;

                if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
                {
                    return null;
                }

                return user;
            }           
        }
    }
}
