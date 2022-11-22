using ApplicationCore.Interfaces;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<List<Tag>> GetAllPlaceTagsAsync();
        Task<List<Tag>> GetAllProductTagsAsync();
        Task<List<Tag>> GetListByIDsAsync(List<Guid> tagIDs);
        Task<List<Tag>> GetPlaceTagListAsync(Guid placeID);
        Task<List<Tag>> GetProductTagListAsync(Guid productID);


        Task<bool> AddProductTagAsync(IEnumerable<ProductTag> list);
        Task<bool> AddPlaceTagAsync(IEnumerable<PlaceTag> list);
        Task<bool> RemoveTagBoundsAsync(Tag tag);

        Task<List<Guid>> FindNotCreatedProductTagsAsync(Product product, List<Guid> tagIDs);
        Task<List<Guid>> FindNotCreatedPlaceTagsAsync(Place place, List<Guid> tagIDs);

    }
}
