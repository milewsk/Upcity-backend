using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upcity.Domain.Models;
using upcity.Domain.ModelsDto;

namespace upcity.Data.UserRepo
{
    public interface IUserRepository2
    {

        User GetUserByGuid(Guid guid);
        User GetUserByEmail(string email);


        User CreateUser(UserDto user);

        User LoginUser(UserDto user);
        
    }
}
