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
        private readonly IAppLogger<Exception> _appLogger;

        public UserRepository(ApplicationDbContext context, IAppLogger<Exception> appLogger) :base(context)
        {
            _context = context;
            _appLogger = appLogger;
        }

        public virtual async Task<User> GetUser(string email, string password)
        {
            try
            {
                return await _context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public virtual async Task<User> GetUserByGuid(Guid id)
        {
            return await _context.Users.Where(x => x.ID == id).FirstOrDefaultAsync();
        }
        public  async Task<bool> IsUserExistWithEmail(string email) 
        {
            return await _context.Users.Where(x => x.Email == email).AnyAsync();
        }
    }
}
