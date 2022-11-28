using Infrastructure.Data.Models;
using Common.Helpers;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Dto.Models;
using Common.Dto;
using Common.Dto.User;

namespace ApplicationCore.Services.Interfaces
{
    public interface IUserService
    {
        Task<Tuple<UserLoginResult, User>> GetUserByEmailAndPasswordAsync(string email, string password);

        Task<Tuple<UserRegisterResult, UserLoginDto>> RegisterUser(CreateUserModel userModel);

        Task<User> GetUserByGuidAsync(Guid ID);

        //User LoginUser(User user);
        Task<bool> IsEmailExist(string email);

        Task<UserDetails> GetUserDetailsAsync(Guid userID);

        Task<UserClaim> GetUserClaimAsync(Guid userID);

        Task<LoyalityProgramAccount> GetUserLoyalityCardAsync(Guid userID);
        //bool CheckCrudentials(string email, string password);

        Task<bool> ChangePasswordAsync(Guid userID, string newPassword);
        Task<bool> DeleteUserAsync(Guid userID);
        Task<List<UserShortcutResult>> GetUserListAsync(string searchString);
    }
}
