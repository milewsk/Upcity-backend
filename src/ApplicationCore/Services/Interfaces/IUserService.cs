using Infrastructure.Data.Models;
using Common.Helpers;
using Common.Enums;
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
        Task<UserDetails> GetUserDetailsAsync(Guid userID);
        //bool CheckCrudentials(string email, string password);
    }
}
