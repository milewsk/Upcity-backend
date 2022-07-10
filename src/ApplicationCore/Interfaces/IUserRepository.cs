using ApplicationCore.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Infrastructure.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
