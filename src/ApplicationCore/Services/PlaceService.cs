using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto;
using Common.Dto.Models;
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
                var placeList =  await _placeRepository.GetListAsync();
                var placeDto = MappingHelper.Mapper.Map<List<Place>, List<PlaceResult>>(placeList);
                return placeDto;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PlaceResult>> GetPlacesCloseAsync()
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

        public async Task<Tuple<PlaceCreatePlaceStatusResult, PlaceResult>> CreatePlaceAsync(CreatePlaceModel model)
        {
            try
            {
                Place newPlace = new Place(model.Name, model.Image, 1);
                await _placeRepository.CreatePlaceAsync(newPlace);

                if(newPlace.ID == null)
                {
                    return new Tuple<PlaceCreatePlaceStatusResult, PlaceResult>(PlaceCreatePlaceStatusResult.IncorrectData, null);
                }

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

                if(newPlaceDetails.ID == null)
                {
                    return new Tuple<PlaceCreatePlaceStatusResult, PlaceResult>(PlaceCreatePlaceStatusResult.IncorrectData, null);
                }
                return null;


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
    }
}
