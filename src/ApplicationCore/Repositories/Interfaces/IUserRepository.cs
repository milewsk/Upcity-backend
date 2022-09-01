using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApplicationCore.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> GetUser(string email, string password);
        Task<User> GetUserByGuid(Guid id);
        Task<bool> IsEmailExist(string email);
    }
}
