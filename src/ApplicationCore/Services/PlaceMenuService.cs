using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Inbox;
using Common.Dto.Place;
using Common.Dto.Tag;
using Infrastructure.Data.Models;
using Infrastructure.Helpers;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ApplicationCore.Services.Interfaces
{
    public class PlaceMenuSerivce : IPlaceMenuService
    {
        private readonly ILogger<PlaceMenuSerivce> _appLogger;
        private readonly IPlaceService _placeService;
        private readonly IPlaceCategoryRepository _placeCategoryRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IJwtService _jwtService;

        public PlaceMenuSerivce(IPlaceService placeService, IPlaceCategoryRepository placeCategoryRepository, IPlaceRepository placeRepository, ILogger<PlaceMenuSerivce> appLogger, IJwtService jwtService)
        {
            _placeService = placeService;
            _placeCategoryRepository = placeCategoryRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
        }

        public async Task<bool> CreateMenuCategoryAsync(CreatePlaceMenuCategoryModel categoryModel)
        {
            try
            {
                var place = await _placeRepository.GetPlaceAsync(categoryModel.PlaceID);

                var placeCategory = new PlaceMenuCategory()
                {
                    PlaceMenuID = place.PlaceMenu.ID,
                    Name = categoryModel.Name,
                };

                await _placeCategoryRepository.CreateCategoryAsync(placeCategory);

                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteMenuCategoryAsync(Guid categoryID)
        {
            try
            {
                return await _placeCategoryRepository.RemoveCategoryAsync(categoryID);
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
