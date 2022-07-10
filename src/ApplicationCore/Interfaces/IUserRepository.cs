using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upcity.Domain.Models;
using upcity.SharedKernel.Interfaces;

namespace Infrastructure.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
