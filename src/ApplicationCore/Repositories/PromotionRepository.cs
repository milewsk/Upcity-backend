using ApplicationCore.Repositories.Interfaces;
using Common.Enums;
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
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PromotionRepository> _logger;

        public PromotionRepository(ApplicationDbContext context, ILogger<PromotionRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreatePromotionAsync(Promotion promotion)
        {
            try
            {
                await _context.Promotions.AddAsync(promotion);
                await _context.SaveChangesAsync();

                if (promotion.ID != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeletePromotionAsync(Guid promotionID)
        {
            try
            {
                var promotion = await _context.Promotions.Where(x => x.ID == promotionID).FirstOrDefaultAsync();
                if (promotion != null)
                {
                    _context.Promotions.Remove(promotion);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Promotion>> GetPromotionsForPlaceAsync(Guid placeID)
        {
            try
            {
                var result = await _context.Promotions.Where(x => x.PlaceID == placeID).OrderBy(x => x.CreationDate).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
