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
using NetTopologySuite.Geometries;
using Common.Utils;

namespace ApplicationCore.Repositories
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlaceRepository> _appLogger;

        public PlaceRepository(ApplicationDbContext context, ILogger<PlaceRepository> appLogger) : base(context, appLogger)
        {
            _context = context;
            _appLogger = appLogger;
        }

        public async Task<List<Place>> GetListAsync()
        {
            try
            {
                return await _context.Places.Include(x => x.Coordinates).Where(x => x.IsActive == 1).Take(50).ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Place>> GetListNearLocationAsync(Coords cords)
        {
            try
            {
                //long = x lat = y
                GeometryFactory geometryFactory = new GeometryFactory();
                Coordinate userGeo = new Coordinate()
                {
                    X = cords.X,
                    Y = cords.Y
                };


                //20km
                var circle = geometryFactory.CreatePoint(userGeo).Buffer(MeterToDegree(20000, userGeo.Y));

                List<Place> result = new List<Place>();
                var list = await _context.Places
                            .Include(x => x.Coordinates)
                            .Include(x => x.PlaceOpeningHours)
                            .Include(x => x.PlaceTags)
                            .Where(x => x.IsActive == 1).ToListAsync();

                foreach (var item in list)
                {
                    if (circle.Covers(item.Coordinates.Location))
                    {
                        result.Add(item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Place>> GetPlacesByCategoryAsync(Coords cords, Guid categoryID)
        {
            try
            {
                List<Place> list = await GetListNearLocationAsync(cords);

                List<Place> result = new List<Place>();

                foreach (var item in list)
                {
                    foreach (var cat in item.PlaceTags)
                    {
                        if (cat.ID == categoryID)
                        {
                            result.Add(item);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                return null;
            }
        }

        //done
        private double MeterToDegree(double meters, double latitude)
        {
            return meters / (111.32 * 1000 * Math.Cos(latitude * (Math.PI / 180)));
        }

        //done
        public async Task<List<Place>> GetListBySearchStringAsync(string searchedText)
        {
            try
            {
                return await _context.Places.Include(x => x.PlaceDetails)
                                            .Include(x => x.Coordinates)
                                            .Where(x => (EF.Functions.Like(x.Name, $"%{searchedText}%")
                                                        || EF.Functions.Like(x.PlaceDetails.City, $"%{searchedText}%")
                                                        || EF.Functions.Like(x.PlaceDetails.Address, $"%{searchedText}%"))
                                                        && x.IsActive == 1).ToListAsync();
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

        public async Task<PlaceOpeningHours> GetPlaceOpeningHourAsync(Place place, DayOfWeek day)
        {
            try
            {
                return await _context.PlaceOpeningHours.Where(x => x.PlaceID == place.ID && x.DayOfWeek == day).FirstOrDefaultAsync();
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

        public async Task<Place> GetPlaceDetailsAsync(Guid placeID)
        {
            try
            {
                var result = await _context.Places
                              .Include(x => x.PlaceDetails)
                              .Include(x => x.PlaceOpeningHours)
                              .Include(x => x.PlaceOpinions)
                              .Include(x => x.PlaceMenu).ThenInclude(x => x.PlaceMenuCategories).ThenInclude(x => x.Products)
                              .Where(x => x.ID == placeID).FirstOrDefaultAsync();

                return result;
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

        public async Task CreatePlaceCoordinatesAsync(Infrastructure.Data.Models.Coordinates coords)
        {
            try
            {
                var xty = coords.Location.Boundary;
                await _context.Coordinates.AddAsync(coords);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreatePlaceOpeningHoursAsync(List<PlaceOpeningHours> openingHoursList)
        {
            try
            {
                await _context.PlaceOpeningHours.AddRangeAsync(openingHoursList);
                await _context.SaveChangesAsync();

                return true;
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
        public async Task CreatePlaceMenuAsync(PlaceMenu placeMenu)
        {
            try
            {
                //zrobić strukturę menu => ma ileś tam kategorii i kategorie mają ileś tam dań
                await _context.AddAsync(placeMenu);
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
