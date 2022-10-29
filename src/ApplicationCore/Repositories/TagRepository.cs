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
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TagRepository> _logger;

        public TagRepository(ApplicationDbContext context, ILogger<TagRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Tag>> GetAllPlaceTagsAsync()
        {
            try
            {
                return await _context.Tags.Where(x =>  x.Type == TagType.Place).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Tag>> GetAllProductTagsAsync()
        {
            try
            {
                return await _context.Tags.Where(x => x.Type == TagType.Product).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Tag>> GetListByIDsAsync(List<Guid> tagIDs)
        {
            try
            {
                return await _context.Tags.Where(x => tagIDs.Contains(x.ID)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Tag>> GetPlaceTagListAsync(Guid placeID)
        {
            try
            {
                var placeTags = await _context.PlaceTags.Where(x => x.PlaceID == placeID).Select(x => x.TagID).ToListAsync();
                return await _context.Tags.Where(x => placeTags.Contains(x.ID)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Tag>> GetProductTagListAsync(Guid productID)
        {
            try
            {
                var productTags = await _context.ProductTags.Where(x => x.ProductID == productID).Select(x => x.TagID).ToListAsync();
                return await _context.Tags.Where(x => productTags.Contains(x.ID)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        //create and delete with all links to them
        public async Task<bool> AddProductTagAsync(IEnumerable<ProductTag> list)
        {
            try
            {
                await _context.ProductTags.AddRangeAsync(list);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> RemoveTagBoundsAsync(Tag tag)
        {
            try
            {
                if (tag.Type == TagType.Place)
                {
                    var list = await _context.PlaceTags.Where(x => x.TagID == tag.ID).ToListAsync();
                    _context.RemoveRange(list);
                }
                else
                {
                    var list = await _context.ProductTags.Where(x => x.TagID == tag.ID).ToListAsync();
                    _context.RemoveRange(list);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> AddPlaceTagAsync(IEnumerable<PlaceTag> list)
        {
            try
            {
                await _context.PlaceTags.AddRangeAsync(list);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Guid>> FindNotCreatedProductTagsAsync(Product product, List<Guid> tagIDs)
        {
            try
            {
                var result = new List<Guid>();

                foreach (var id in tagIDs)
                {
                    bool isExist = await _context.ProductTags.Where(x => id == x.TagID && product.ID == x.ProductID).AnyAsync();
                    if (!isExist)
                    {
                        result.Add(id);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Guid>> FindNotCreatedPlaceTagsAsync(Place place, List<Guid> tagIDs)
        {
            try
            {
                var result = new List<Guid>();

                foreach (var id in tagIDs)
                {
                    bool isExist = await _context.PlaceTags.Where(x => id == x.TagID && place.ID == x.PlaceID).AnyAsync();
                    if (!isExist)
                    {
                        result.Add(id);
                    }
                }

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
