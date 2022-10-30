﻿using Infrastructure.Helpers;
using Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helpers.Enums;
using Common.Dto.Models;
using Common.Dto.Place;

namespace ApplicationCore.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<Tuple<PlaceCreatePlaceStatusResult, PlaceDetailsResult>> CreatePlaceAsync(CreatePlaceModel model);
        Task<Tuple<PlaceEditPlaceStatusResult, bool>> EditPlaceAsync();

        Task<List<PlaceResult>> GetPlacesAsync();
        Task<List<PlaceShortcutResult>> GetPlacesByCategoryAsync(string latitude, string longitude, string categoryID);
        Task<List<PlaceShortcutResult>> GetPlacesListBySearchStringAsync(string searchString, string latitude, string longitude);
        Task<List<PlaceShortcutResult>> GetPlaceListNearLocationAsync(string latitude, string longitude);


        Task<PlaceMenuResult> GetPlaceMenuResultAsync(Guid placeID);
        Task<bool> CreatePlaceMenuCategoryAsync(CreatePlaceMenuCategoryModel categoryModel);
    }
}
