using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto;
using Common.Dto.Models;
using Common.Dto.Place;
using Common.Utils;
using Infrastructure.Data.Models;
using Infrastructure.Helpers;
using Infrastructure.Helpers.Enums;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Distance;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ILogger<PlaceService> _appLogger;
        private readonly IPlaceRepository _placeRepository;
        private readonly IJwtService _jwtService;

        public PlaceService(IPlaceRepository placeRepository, ILogger<PlaceService> appLogger, IJwtService jwtService)
        {
            _placeRepository = placeRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
        }

        public async Task<List<PlaceResult>> GetPlacesAsync()
        {
            try
            {
                var placeList = await _placeRepository.GetListAsync();
                var placeDto = MappingHelper.Mapper.Map<List<Place>, List<PlaceResult>>(placeList);
                return placeDto;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PlaceShortcutResult>> GetPlaceListNearLocationAsync(string latitude, string longitude)
        {
            try
            {
                Coords cords = new Coords() { Y = Convert.ToDouble(longitude), X = Convert.ToDouble(latitude) };

                List<PlaceShortcutResult> placeResults = new List<PlaceShortcutResult>();

                var placeList = await _placeRepository.GetListNearLocationAsync(cords);
                foreach (Place place in placeList)
                {
                    // count distance
                    var distance = CalculateDistance(latitude, longitude, place.Coordinates);
                    //coords
                    var placeCords = new Coords() { X = place.Coordinates.Location.Coordinate.X, Y = place.Coordinates.Location.Coordinate.X };

                    PlaceShortcutResult result = new PlaceShortcutResult()
                    {
                        Name = place.Name,
                        Distance = distance,
                        OpeningHour = $"{place.PlaceOpeningHours.Opens.Hours}:{place.PlaceOpeningHours.Opens.Minutes}",
                        CloseHour = $"{place.PlaceOpeningHours.Closes.Hours}:{place.PlaceOpeningHours.Closes.Minutes}",
                        Image = place.Image,
                        PlaceID = place.ID,
                        Coords = placeCords,
                    };

                    placeResults.Add(result);
                }

                return placeResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PlaceShortcutResult>> GetPlacesByCategoryAsync(string latitude, string longitude, string categoryID)
        {
            try
            {
                Coords cords = new Coords() { Y = Convert.ToDouble(longitude), X = Convert.ToDouble(latitude) };

                List<PlaceShortcutResult> placeResults = new List<PlaceShortcutResult>();

                var placeList = await _placeRepository.GetPlacesByCategoryAsync(cords, Guid.Parse(categoryID));
                foreach (Place place in placeList)
                {
                    // count distance
                    var distance = CalculateDistance(latitude, longitude, place.Coordinates);
                    //coords
                    var placeCords = new Coords() { X = place.Coordinates.Location.Coordinate.X, Y = place.Coordinates.Location.Coordinate.X };

                    PlaceShortcutResult result = new PlaceShortcutResult()
                    {
                        Name = place.Name,
                        Distance = distance,
                        OpeningHour = $"{place.PlaceOpeningHours.Opens.Hours}:{place.PlaceOpeningHours.Opens.Minutes}",
                        CloseHour = $"{place.PlaceOpeningHours.Closes.Hours}:{place.PlaceOpeningHours.Closes.Minutes}",
                        Image = place.Image,
                        PlaceID = place.ID,
                        Coords = placeCords,
                    };

                    placeResults.Add(result);
                }

                return placeResults;
               
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PlaceShortcutResult>> GetPlacesListBySearchStringAsync(string searchString, string latitude, string longitude)
        {
            try
            {
                var placeList = await _placeRepository.GetListBySearchStringAsync(searchString);

                GeometryFactory geometryFactory = new GeometryFactory();
                Coordinate userGeo = new Coordinate
                {
                    X = Convert.ToDouble(longitude),
                    Y = Convert.ToDouble(latitude)
                };

                var userPoint = geometryFactory.CreatePoint(userGeo);


                List<PlaceShortcutResult> placeResults = new List<PlaceShortcutResult>();
                foreach (Place place in placeList)
                {

                    var distance = place.Coordinates.Location.Distance(userPoint);
                    PlaceShortcutResult result = new PlaceShortcutResult()
                    {
                        Name = place.Name,
                        Distance = distance,
                        OpeningHour = $"{place.PlaceOpeningHours.Opens.Hours}:{place.PlaceOpeningHours.Opens.Minutes}",
                        CloseHour = $"{place.PlaceOpeningHours.Closes.Hours}:{place.PlaceOpeningHours.Closes.Minutes}",
                        Image = place.Image,
                        PlaceID = place.ID,
                        Coords = { X = place.Coordinates.Location.X, Y = place.Coordinates.Location.Y },

                    };

                    placeResults.Add(result);
                }

                return placeResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Tuple<PlaceCreatePlaceStatusResult, bool>> CreatePlaceAsync(CreatePlaceModel model)
        {
            try
            {
                Place newPlace = new Place(model.Name, model.Image, 1);
                await _placeRepository.CreatePlaceAsync(newPlace);

                if (newPlace.ID == null)
                {
                    return new Tuple<PlaceCreatePlaceStatusResult, bool>(PlaceCreatePlaceStatusResult.IncorrectData, false);
                }

                // create Place Details
                PlaceDetails newPlaceDetails = new PlaceDetails()
                {
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    PlaceID = newPlace.ID,
                    Description = model.Description,
                    Address = model.Address,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    NIP = model.NIP,
                    PhoneNumber = model.PhoneNumber,
                    RegisterFirm = model.RegisterFirm,
                };

                await _placeRepository.CreatePlaceDetailsAsync(newPlaceDetails);

                if (newPlaceDetails.ID == null)
                {
                    return new Tuple<PlaceCreatePlaceStatusResult, bool>(PlaceCreatePlaceStatusResult.IncorrectData, false);
                }

                // create Place Tags
                List<PlaceTag> placeTags = new List<PlaceTag>();

                foreach (var placeTagID in model.TagIDs)
                {
                    PlaceTag newPlaceTag = new PlaceTag()
                    {
                        CreationDate = DateTime.Now,
                        LastModificationDate = DateTime.Now,
                        PlaceID = newPlace.ID,
                        TagID = placeTagID,
                    };

                    placeTags.Add(newPlaceTag);
                }

                await _placeRepository.CreatePlaceTagsAsync(placeTags);

                // create Place Menu
                PlaceMenu newPlaceMenu = new PlaceMenu()
                {
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    PlaceID = newPlace.ID
                };

                await _placeRepository.CreatePlaceMenuAsync(newPlaceMenu);

                if (newPlaceMenu.ID == null)
                {
                    return new Tuple<PlaceCreatePlaceStatusResult, bool>(PlaceCreatePlaceStatusResult.IncorrectData, false);
                }

                return new Tuple<PlaceCreatePlaceStatusResult, bool>(PlaceCreatePlaceStatusResult.Ok, true);
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        private double CalculateDistance(string latitude, string longitude, Infrastructure.Data.Models.Coordinates placeCoordinates)
        {
            try
            {
                GeometryFactory geometryFactory = new GeometryFactory();
                Coordinate userGeo = new Coordinate
                {
                    X = Convert.ToDouble(longitude),
                    Y = Convert.ToDouble(latitude)
                };

                var userPoint = geometryFactory.CreatePoint(userGeo);

                var distance = placeCoordinates.Location.Distance(userPoint);

                return distance;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public Task<Tuple<PlaceEditPlaceStatusResult, bool>> EditPlaceAsync()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
        public Task<PlaceMenuResult> GetPlaceMenuResultAsync(Guid placeID)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<bool> CreateOpeningHoursAsync(CreateOpeningHoursModelList modelList)
        {
            try
            {
                List<PlaceOpeningHours> placeOpeningHours = new List<PlaceOpeningHours>();

                foreach(var item in modelList.Items)
                {
                    //var itemToAdd = new PlaceOpeningHours()
                    //{
                    //    PlaceID = Guid.Parse(modelList.PlaceID),
                    //    CreationDate = DateTime.Now,
                    //    LastModificationDate = DateTime.Now,
                    //    Opens = item.OpenTime,
                    //    Closes = item.CloseTime,
                    //    DayOfWeek = item.DayOfWeek
                    //};

                    //placeOpeningHours.Add(itemToAdd);
                }

                return await _placeRepository.CreatePlaceOpeningHoursAsync(placeOpeningHours); ;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
        
        public Task<bool> CreatePlaceMenuCategoryAsync(CreatePlaceMenuCategoryModel categoryModel)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
