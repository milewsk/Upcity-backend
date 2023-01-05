using Infrastructure.Helpers;
using Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helpers.Enums;
using Common.Dto.Models;
using Common.Dto.Place;
using Common.Dto.User;
using Microsoft.AspNetCore.Hosting;

namespace ApplicationCore.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<Tuple<PlaceCreatePlaceStatusResult, bool>> CreatePlaceAsync(CreatePlaceModel model);
        Task<Tuple<PlaceCreatePlaceStatusResult, bool>> CreateOwnerPlaceAsync(CreateOwnerWithPlaceModel model, IWebHostEnvironment hostEnviroment);

        Task<bool> CreatePlaceMenuCategoryAsync(CreatePlaceMenuCategoryModel categoryModel);
        Task<Tuple<PlaceEditPlaceStatusResult, bool>> EditPlaceAsync();

        Task<List<PlaceResult>> GetPlacesAsync();
        Task<PlaceDetailsResult> GetPlaceDetailsAsync(Guid placeID, Guid userID);

        Task<List<PlaceShortcutResult>> GetPlacesByCategoryAsync(string latitude, string longitude, Guid tagID, Guid userID);
        Task<List<PlaceShortcutResult>> GetPlacesListBySearchStringAsync(string searchString, string latitude, string longitude, Guid userID);
        Task<List<PlaceShortcutResult>> GetPlaceListNearLocationAsync(string latitude, string longitude, Guid userID);
        Task<List<PlaceShortcutResult>> GetFavouritePlaceListAsync(Guid userID);
   
        Task<PlaceMenuResult> GetPlaceMenuResultAsync(Guid placeID);

        Task<bool> AddToFavouriteAsync(Guid placeID, Guid userID);
        Task<Guid> GetOwnerPlaceIDAsync(Guid ownerID);


        //Opening Hours
        Task<GetPlaceOpeningHourListResult> GetPlaceOpeningHoursAsync(Guid placeID);
        Task<bool> UpdateOpeningHoursAsync(UpdatePlaceOpeningHourListModel modelList);     
        Task<bool> CreateOpeningHoursAsync(Guid placeID);
    }
}
