using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }

        public virtual async Task<User> GetUser(string email, string password)
        {
            return await _context.Set<User>().Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public virtual async Task<User> GetUserByGuid(Guid id)
        {
            return await _context.Set<User>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }
        public  async Task<bool> IsEmailExist(string email) 
        {
            return await _context.Set<User>().Where(x => x.Email == email).AnyAsync();
        }
    }
}
