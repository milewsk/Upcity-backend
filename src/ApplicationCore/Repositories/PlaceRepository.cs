using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Data;
using Common.Dto.Models;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlaceRepository> _appLogger;

        public PlaceRepository(ApplicationDbContext context, ILogger<PlaceRepository> appLogger): base(context, appLogger)
        {
            _context = context;
            _appLogger = appLogger;
        }

        public async Task<List<Place>> GetListAsync()
        {
            try
            {
                return await _context.Places.Where(x => x.IsActive == 1).Take(50).ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Place>> GetListBySearchStringAsync(string searchedText)
        {
            try
            {
                return await _context.Places.Where(x => x.Name == searchedText && x.IsActive == 1).ToListAsync();  
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Place>> GetListByCategoryAsync(PlaceTag tag)
        {
            try
            {
                return await _context.Places.Where(x => x.PlaceTags.Contains(tag) && x.IsActive == 1).ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<PlaceDetails> GetPlaceDetailsAsync(Guid placeID)
        {
            try
            {
                return await _context.PlacesDetails.Where(x => x.PlaceID == placeID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        //do zmiany
        public async Task<PlaceMenu> GetPlaceMenuAsync(Guid placeID)
        {
            try
            {
                //zrobić strukturę menu => ma ileś tam kategorii i kategorie mają ileś tam dań
               return await _context.PlaceMenus.Where(x => x.PlaceID == placeID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreatePlaceAsync(Place newPlace)
        {
            try
            {
                //zrobić strukturę menu => ma ileś tam kategorii i kategorie mają ileś tam dań
                await _context.AddAsync(newPlace);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreatePlaceDetailsAsync(PlaceDetails newPlaceDetails)
        {
            try
            {
                //zrobić strukturę menu => ma ileś tam kategorii i kategorie mają ileś tam dań
                await _context.AddAsync(newPlaceDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreatePlaceOpeningHoursAsync(List<PlaceOpeningHours> openingHoursList)
        {
            try
            {
                //zrobić strukturę menu => ma ileś tam kategorii i kategorie mają ileś tam dań
                await _context.AddRangeAsync(openingHoursList);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreatePlaceTagsAsync(List<PlaceTag> placeTags)
        {
            try
            {
                //zrobić strukturę menu => ma ileś tam kategorii i kategorie mają ileś tam dań
                await _context.AddRangeAsync(placeTags);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
