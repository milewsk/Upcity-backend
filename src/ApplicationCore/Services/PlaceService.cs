using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto;
using Common.Dto.Models;
using Common.Dto.Place;
using Common.Dto.Product;
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
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ILogger<PlaceService> _appLogger;
        private readonly IPlaceRepository _placeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserLikeRepository _userLikeRepository;
        private readonly IJwtService _jwtService;

        public PlaceService(IPlaceRepository placeRepository, IUserRepository userRepository,
            IUserLikeRepository userLikeRepository,
            ILogger<PlaceService> appLogger, IJwtService jwtService)
        {
            _placeRepository = placeRepository;
            _userRepository = userRepository;
            _userLikeRepository = userLikeRepository;
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

        public async Task<List<PlaceShortcutResult>> GetPlaceListNearLocationAsync(string latitude, string longitude, Guid userID)
        {
            try
            {
                NumberFormatInfo provider = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = ","
                };

                Coords cords = new Coords()
                {
                    Y = double.Parse(latitude, provider),
                    X = double.Parse(longitude, provider)
                };

                List<PlaceShortcutResult> placeResults = new List<PlaceShortcutResult>();

                var placeList = await _placeRepository.GetListNearLocationAsync(cords);

                if (placeList == null)
                {
                    return null;
                }

                foreach (Place place in placeList)
                {
                    bool isLiked = await _userLikeRepository.CheckExistance(userID, place.ID);
                    // opening day
                    DayOfWeek day = DateTime.Now.DayOfWeek;
                    var openingHours = await _placeRepository.GetPlaceOpeningHourAsync(place, day);
                    // count distance
                    var distance = CalculateDistance(latitude, longitude, place.Coordinates);
                    //coords
                    var placeCords = new Coords() { X = place.Coordinates.Location.Coordinate.X, Y = place.Coordinates.Location.Coordinate.Y };

                    PlaceShortcutResult result = new PlaceShortcutResult()
                    {
                        Name = place.Name,
                        Distance = distance,
                        OpeningHour = openingHours != null ? $"{openingHours.Opens.Hours}:{openingHours.Opens.Minutes}" : "-",
                        CloseHour = openingHours != null ? $"{openingHours.Closes.Hours}:{openingHours.Closes.Minutes}" : "-",
                        IsOpen = openingHours != null,
                        Image = place.Image,
                        IsLiked = isLiked,
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

        public async Task<List<PlaceShortcutResult>> GetPlacesByCategoryAsync(string latitude, string longitude, Guid tagID, Guid userID)
        {
            try
            {
                NumberFormatInfo provider = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = ","
                };

                Coords cords = new Coords()
                {
                    Y = double.Parse(latitude, provider),
                    X = double.Parse(longitude, provider)
                };

                List<PlaceShortcutResult> placeResults = new List<PlaceShortcutResult>();

                var placeList = await _placeRepository.GetPlacesByCategoryAsync(cords, tagID);
                foreach (Place place in placeList)
                {
                    bool isLiked = await _userLikeRepository.CheckExistance(userID, place.ID);
                    // opening day
                    DayOfWeek day = DateTime.Now.DayOfWeek;
                    var openingHours = await _placeRepository.GetPlaceOpeningHourAsync(place, day);
                    // count distance
                    var distance = CalculateDistance(latitude, longitude, place.Coordinates);
                    //coords
                    var placeCords = new Coords() { X = place.Coordinates.Location.Coordinate.X, Y = place.Coordinates.Location.Coordinate.X };

                    PlaceShortcutResult result = new PlaceShortcutResult()
                    {
                        Name = place.Name,
                        Distance = distance,
                        OpeningHour = openingHours != null ? $"{openingHours.Opens.Hours}:{openingHours.Opens.Minutes}" : "-",
                        CloseHour = openingHours != null ? $"{openingHours.Closes.Hours}:{openingHours.Closes.Minutes}" : "-",
                        IsOpen = openingHours != null,
                        IsLiked = isLiked,
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

        public async Task<List<PlaceShortcutResult>> GetPlacesListBySearchStringAsync(string searchString, string latitude, string longitude, Guid userID)
        {
            try
            {
                var placeList = await _placeRepository.GetListBySearchStringAsync(searchString);

                GeometryFactory geometryFactory = new GeometryFactory() { };

                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";

                var userGeo = new Coordinate() { };
                userGeo.X = double.Parse(longitude, provider);
                userGeo.Y = double.Parse(latitude, provider);
                var userPoint = geometryFactory.CreatePoint(userGeo);


                List<PlaceShortcutResult> placeResults = new List<PlaceShortcutResult>();
                foreach (Place place in placeList)
                {
                    DayOfWeek day = DateTime.Now.DayOfWeek;
                    var openingHours = await _placeRepository.GetPlaceOpeningHourAsync(place, day);
                    bool isLiked = await _userLikeRepository.CheckExistance(userID, place.ID);
                    bool showHours = false;
                    var openHourString = "-";
                    var closeHourString = "-";

                    if (openingHours != null)
                    {
                        showHours = true;
                        openHourString = $"{openingHours.Opens.Hours}:{openingHours.Opens.Minutes}";
                        closeHourString = $"{openingHours.Closes.Hours}:{openingHours.Closes.Minutes}";
                    }

                    var distance = place.Coordinates.Location.Distance(userPoint);
                    PlaceShortcutResult result = new PlaceShortcutResult()
                    {
                        Name = place.Name,
                        Distance = Math.Truncate(distance * 100) / 100,
                        OpeningHour = openHourString,
                        CloseHour = closeHourString,
                        IsOpen = showHours,
                        IsLiked = isLiked,
                        Image = place.Image,
                        PlaceID = place.ID,
                        Coords = new Coords() { X = place.Coordinates.Location.Coordinate.X, Y = place.Coordinates.Location.Coordinate.Y }
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
                    MaxSeatNumber = model.MaxSeatNumber
                };

                await _placeRepository.CreatePlaceDetailsAsync(newPlaceDetails);

                if (newPlaceDetails.ID == null)
                {
                    return new Tuple<PlaceCreatePlaceStatusResult, bool>(PlaceCreatePlaceStatusResult.IncorrectData, false);
                }

                //create Coordinates 

                Infrastructure.Data.Models.Coordinates coords = new Infrastructure.Data.Models.Coordinates()
                {
                    Location = new Point(model.Longitude, model.Latitude),
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    PlaceID = newPlace.ID
                };

                await _placeRepository.CreatePlaceCoordinatesAsync(coords);

                if (coords.ID == null)
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
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";

                GeometryFactory geometryFactory = new GeometryFactory();
                var userGeo = new Coordinate() { };
                userGeo.X = double.Parse(longitude, provider);
                userGeo.Y = double.Parse(latitude, provider);


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

                foreach (var item in modelList.Items)
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

        public async Task<bool> CreatePlaceMenuCategoryAsync(CreatePlaceMenuCategoryModel categoryModel)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<PlaceDetailsResult> GetPlaceDetailsAsync(Guid placeID, Guid userID)
        {
            try
            {
                var place = await _placeRepository.GetPlaceDetailsAsync(placeID);
                bool isLiked = await _userLikeRepository.CheckExistance(userID, placeID);

                //coords
                var placeCords = new Coords() { X = place.Coordinates.Location.Coordinate.X, Y = place.Coordinates.Location.Coordinate.Y };

                var placeDetails = MappingHelper.Mapper.Map<PlaceDetails, PlaceResult>(place.PlaceDetails);
                placeDetails.Name = place.Name;

                List<PlaceCategoryResult> categoryResults = new List<PlaceCategoryResult>();
                foreach (var category in place.PlaceMenu.PlaceMenuCategories)
                {
                    List<ProductResult> productResults = new List<ProductResult>();
                    foreach (var product in category.Products)
                    {
                        ProductResult prodResult = new ProductResult() { Name = product.Name, Description = product.Description, DiscountPrice = product.DiscountPrice, Price = product.Price };
                        productResults.Add(prodResult);
                    }

                    PlaceCategoryResult item = new PlaceCategoryResult() { Name = category.Name, ProductResults = productResults };
                    categoryResults.Add(item);
                }

                List<OpeningTime> openingTimes = new List<OpeningTime>();

                foreach (var time in place.PlaceOpeningHours)
                {
                    OpeningTime item = new OpeningTime()
                    {
                        OpenTime = $"{time.Opens.Hours}-{time.Opens.Minutes}",
                        CloseTime = $"{time.Closes.Hours}-{time.Closes.Minutes}",
                        Day = time.DayOfWeek,

                    };
                    openingTimes.Add(item);
                }

                PlaceDetailsResult result = new PlaceDetailsResult()
                {
                    PlaceID = place.ID,
                    IsLiked = isLiked,
                    Coords = placeCords,
                    PlaceResult = placeDetails,
                    PlaceMenuResult = new PlaceMenuResult()
                    {
                        PlaceCategoryResults = categoryResults
                    },
                    PlaceOpeningHours = new PlaceOpeningHoursModel()
                    {
                        OpeningTimes = openingTimes
                    }
                };

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PlaceShortcutResult>> GetFavouritePlaceListAsync(Guid userID)
        {
            try
            {
                var placeList = await _placeRepository.GetLikedListAsync(userID);

                List<PlaceShortcutResult> placeResults = new List<PlaceShortcutResult>();
                foreach (Place place in placeList)
                {
                    DayOfWeek day = DateTime.Now.DayOfWeek;
                    var openingHours = await _placeRepository.GetPlaceOpeningHourAsync(place, day);
                    bool showHours = false;
                    var openHourString = "-";
                    var closeHourString = "-";

                    if (openingHours != null)
                    {
                        showHours = true;
                        openHourString = $"{openingHours.Opens.Hours}:{openingHours.Opens.Minutes}";
                        closeHourString = $"{openingHours.Closes.Hours}:{openingHours.Closes.Minutes}";
                    }

                    PlaceShortcutResult result = new PlaceShortcutResult()
                    {
                        Name = place.Name,
                        Distance = null,
                        OpeningHour = openHourString,
                        CloseHour = closeHourString,
                        IsOpen = showHours,
                        IsLiked = true,
                        Image = place.Image,
                        PlaceID = place.ID,
                        Coords = new Coords() { X = place.Coordinates.Location.Coordinate.X, Y = place.Coordinates.Location.Coordinate.Y }
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
        public async Task<bool> AddToFavouriteAsync(Guid placeID, Guid userID)
        {
            try
            {
                User user = await _userRepository.GetUserByGuid(userID);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                bool isExistingAlready = await _userLikeRepository.CheckExistance(userID, placeID);
                if (isExistingAlready)
                {
                    return await _userLikeRepository.RemoveUserLikeAsync(userID, placeID);
                }
                else
                {
                    return await _userLikeRepository.AddUserLikeAsync(userID, placeID);
                }

            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
