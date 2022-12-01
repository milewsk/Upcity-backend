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
    public class PlaceCategoryRepository : GenericRepository<PlaceMenuCategory>, IPlaceCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlaceCategoryRepository> _logger;

        public PlaceCategoryRepository(ApplicationDbContext context, ILogger<PlaceCategoryRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> RemoveCategoryAsync(Guid categoryID)
        {
            try
            {
                var category = await _context.PlaceMenuCategories.Where(x => x.ID == categoryID).FirstOrDefaultAsync();
                if(category == null)
                {
                    return true;
                }

                _context.PlaceMenuCategories.Remove(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateCategoryAsync(PlaceMenuCategory category)
        {
            try
            {
                if (category == null)
                {
                    return false;
                }

                await _context.PlaceMenuCategories.AddAsync(category);
                await _context.SaveChangesAsync();

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
