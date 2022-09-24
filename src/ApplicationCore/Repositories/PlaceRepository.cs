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
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public PlaceRepository(ApplicationDbContext context, IUserRepository userRepository, ILogger<PlaceRepository> appLogger, IJwtService jwtService): base(context, appLogger)
        {
            _context = context;
            _userRepository = userRepository;
            _jwtService = jwtService;
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
                return null;
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
                return null;
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
                return null;
            }
        }

        //do zmiany
        public async Task<PlaceDetails> GetPlaceMenuAsync(Guid placeID)
        {
            try
            {
                //zrobić strukturę menu => ma ileś tam kategorii i kategorie mają ileś tam dań
                return await _context.PlacesDetails.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                return null;
            }
        }
    }
}
