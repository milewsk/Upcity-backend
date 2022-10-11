﻿using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto;
using Common.Dto.Models;
using Common.Dto.Place;
using Infrastructure.Data.Models;
using Infrastructure.Helpers;
using Infrastructure.Helpers.Enums;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
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

        public async Task<List<PlaceResult>> GetPlacesNearUserLocationAsync(string latitude, string longitude)
        {
            try
            {
                double[] cords = { Convert.ToDouble(longitude), Convert.ToDouble(latitude) };
                var placeList = await _placeRepository.GetListNearLocationAsync(cords);
                var placeResults = MappingHelper.Mapper.Map<List<Place>, List<PlaceResult>>(placeList);

                return placeResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
        
        public async Task<List<PlaceResult>> GetPlacesByCategoryAsync(string latitude, string longitude, string categoryID)
        {
            try
            {
                double[] cords = { Convert.ToDouble(longitude), Convert.ToDouble(latitude) };
                var placeList = await _placeRepository.GetPlacesByCategoryAsync(cords, Guid.Parse(categoryID));
                var placeResults = MappingHelper.Mapper.Map<List<Place>, List<PlaceResult>>(placeList);

                return placeResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PlaceShortcutResult>> GetPlacesListBySearchStringAsync(string searchString)
        {
            try
            {
                var placeList = await _placeRepository.GetListBySearchStringAsync(searchString);
                var placeResults = MappingHelper.Mapper.Map<List<Place>, List<PlaceShortcutResult>>(placeList);

                return placeResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Tuple<PlaceCreatePlaceStatusResult, PlaceDetailsResult>> CreatePlaceAsync(CreatePlaceModel model)
        {
            try
            {
                // create Place
                Place newPlace = new Place(model.Name, model.Image, 1);
                await _placeRepository.CreatePlaceAsync(newPlace);

                if (newPlace.ID == null)
                {
                    return new Tuple<PlaceCreatePlaceStatusResult, PlaceDetailsResult>(PlaceCreatePlaceStatusResult.IncorrectData, null);
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
                    return new Tuple<PlaceCreatePlaceStatusResult, PlaceDetailsResult>(PlaceCreatePlaceStatusResult.IncorrectData, null);
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
                    return new Tuple<PlaceCreatePlaceStatusResult, PlaceDetailsResult>(PlaceCreatePlaceStatusResult.IncorrectData, null);
                }

                // create PLace

                //basicly we want to return result to redirect to place details


                var result = new PlaceDetailsResult()
                {

                };


                return new Tuple<PlaceCreatePlaceStatusResult, PlaceDetailsResult>(PlaceCreatePlaceStatusResult.Ok, result);
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
