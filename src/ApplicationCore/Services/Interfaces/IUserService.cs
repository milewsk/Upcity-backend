using Infrastructure.Data.Models;
using Infrastructure.Helpers;
using Infrastructure.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IUserService
    {
        Task<Tuple<UserLoginResult, User>> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<Tuple<UserRegisterResult, string>> RegisterUser(string email, string password);
        Task<User> GetUserByGuidAsync(Guid ID);
        //User LoginUser(User user);
        Task<bool> IsEmailExist(string email);
        Task<Tuple<UserLoginResult, bool>> CreateUserDetails(string jwt);

        //bool CheckCrudentials(string email, string password);
    }
}
