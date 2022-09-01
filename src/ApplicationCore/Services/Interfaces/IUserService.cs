using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string password);
        Task<User> CreateUser(string email, string password);
        Task<User> GetUserByGuid(Guid ID);
        //User LoginUser(User user);
        Task<bool> IsEmailExist(string email);

    }
}
