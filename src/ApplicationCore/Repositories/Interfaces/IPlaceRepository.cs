using ApplicationCore.Interfaces;
using Common.Utils;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface IPlaceRepository : IRepository<Place>
    {
        Task<List<Place>> GetListAsync();
        Task<List<Place>> GetListNearLocationAsync(Coords coords);
        Task<List<Place>> GetListBySearchStringAsync(string searchedText);
        Task<PlaceMenu> GetPlaceMenuAsync(Guid placeID);
        Task<List<Place>> GetListByCategoryAsync(PlaceTag tag);

        Task<List<Place>> GetPlacesByCategoryAsync(Coords coords, Guid categoryID);
        Task CreatePlaceAsync(Place newPlace);
        Task CreatePlaceDetailsAsync(PlaceDetails newPlaceDetails);
        Task<bool> CreatePlaceOpeningHoursAsync(List<PlaceOpeningHours> openingHoursList);
        Task CreatePlaceTagsAsync(List<PlaceTag> placeTags);
        Task CreatePlaceMenuAsync(PlaceMenu placeMenu);
    }
}
