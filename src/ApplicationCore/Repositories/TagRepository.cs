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
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TagRepository> _logger;

        public TagRepository(ApplicationDbContext context, ILogger<TagRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Tag>> GetTagListAsync()
        {
            try
            {
                return await _context.Tags.Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<List<Tag>> GetPlaceTagListAsync(Guid placeID)
        {
            try
            {
                var placeTags = await _context.PlaceTags.Where(x => x.PlaceID == placeID).Select(x => x.TagID).ToListAsync();
                return await _context.Tags.Where(x => x.IsActive == 1 && placeTags.Contains(x.ID)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Tag>> GetProductTagListAsync(Guid productID)
        {
            try
            {
                var productTags = await _context.ProductTags.Where(x => x.ProductID == productID).Select(x => x.TagID).ToListAsync();
                return await _context.Tags.Where(x => x.IsActive == 1 && productTags.Contains(x.ID)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        //create and delete with all links to them
    }
}
