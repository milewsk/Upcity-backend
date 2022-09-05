using ApplicationCore.Services.Interfaces;
using Infrastructure.Data.Dto;
using Infrastructure.Helpers;
using Infrastructure.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PlaceService : IPlaceService
    {
        public async Task<Tuple<PlaceCreatePlaceStatusResult, PlaceDto>> CreatePlaceAsync()
        {
            return null;
        }

        public Task<Tuple<PlaceEditPlaceStatusResult, bool>> EditPlaceAsync()
        {
            return null;
        }
    }
}
