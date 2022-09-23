using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Infrastructure.Data.Dto;
using Infrastructure.Helpers;
using Infrastructure.Helpers.Enums;
using Infrastructure.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ILogger _appLogger;
        private readonly IPlaceRepository _placeRepository;
        private readonly IJwtService _jwtService;

        public PlaceService(IPlaceRepository placeRepository, ILogger appLogger, IJwtService jwtService)
        {
            _placeRepository = placeRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
        }

        public async Task<List<PlaceDto>> GetPlacesAsync()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                _appLogger.Error(ex);
                return null;
            }
        }
        public async Task<Tuple<PlaceCreatePlaceStatusResult, PlaceDto>> CreatePlaceAsync()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                _appLogger.Error(ex);
                return null;
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
                _appLogger.Error(ex);
                return null;
            }
        }
    }
}
