using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public class UserLikeRepository : GenericRepository<UserLike>, IUserLikeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserLikeRepository> _logger;

        public UserLikeRepository(ApplicationDbContext context, ILogger<UserLikeRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<UserLike>> GetUserLikeListAsync(Guid userID)
        {
            try
            {
                return await _context.UserLikes.Where(x => x.UserID == userID).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CheckExistance(Guid userID, Guid placeID)
        {
            try
            {
                return await _context.UserLikes.Where(x => x.UserID == userID && x.PlaceID == placeID).AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> AddUserLikeAsync(Guid userID, Guid placeID)
        {
            try
            {
                UserLike like = new UserLike()
                {
                    PlaceID = placeID,
                    UserID = userID,
                };

                await _context.UserLikes.AddAsync(like);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> RemoveUserLikeAsync(Guid userID, Guid placeID)
        {
            try
            {
                var like = _context.UserLikes.Where(x => x.UserID == userID && x.PlaceID == placeID).FirstOrDefault();
                if (like != null)
                {
                    _context.UserLikes.Remove(like);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
