using ApplicationCore.Interfaces;
using Common.Enums;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ApplicationCore.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(string email, string password);
        Task<User> GetUserByGuid(Guid id);
        Task<bool> IsUserExistWithEmail(string email);
        Task CreateUserDetailsAsync(UserDetails userDetails);
        Task CreateUserClaimAsync(UserClaim claim);
        Task CreateUserLoyalityAccountAsync(LoyalityProgramAccount account);
        Task<LoyalityProgramAccount> GetUserLoyalityCardAsync(Guid userID);
        Task<UserClaim> GetUserClaimAsync(Guid userID);
        Task<UserDetails> GetUserDetailsAsync(Guid userID);
        //Task AddClaims(User user,List<Claim> claims);
    }
}
