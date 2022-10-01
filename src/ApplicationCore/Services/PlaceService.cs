using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto;
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

        public async Task<Tuple<PlaceCreatePlaceStatusResult, PlaceResult>> CreatePlaceAsync()
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
