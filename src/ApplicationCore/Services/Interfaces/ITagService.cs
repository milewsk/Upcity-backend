using Common.Dto.Tag;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
   public interface ITagService
    {
        // we could simplyfy there and do it in one method
        Task<List<TagResult>> GetPlaceTagListAsync();
        Task<bool> CreateTagAsync(CreateTagModel model);
        Task<bool> DeleteTagAsync(Guid tagID);
        Task<bool> CreatePlaceTagsAsync(Place place, List<Guid> tagIDs);
        Task<bool> CreateProductTagsAsync(Product product, List<Guid> tagIDs);
    }
}
