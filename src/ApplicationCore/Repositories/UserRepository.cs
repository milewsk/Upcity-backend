﻿using ApplicationCore.Interfaces;
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
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Common.Enums;

namespace ApplicationCore.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<User> GetUser(string email, string password)
        {
            try
            {
                return await _context.Users.Include(x => x.UserDetails).Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<UserClaim> GetUserClaimAsync(Guid userID)
        {
            try
            {
                return await _context.UserClaims.Where(x => x.UserID == userID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public virtual async Task<User> GetUserByGuid(Guid id)
        {
            try
            {
                return await _context.Users.Where(x => x.ID == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> IsUserExistWithEmail(string email)
        {
            try
            {
                return await _context.Users.Where(x => x.Email == email).AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<UserDetails> GetUserDetailsAsync(Guid userID)
        {
            try
            {
                return await _context.UsersDetails.Where(x => x.UserID == userID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        //    public async Task AddClaims(User user, List<Claim> claims)
        //    {
        //        try
        //        {
        //            _appLogger.LogError("siema");
        //            return await _context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            _appLogger.LogError(ex.Message);
        //            return null;
        //        }
        //    }
    }
}
