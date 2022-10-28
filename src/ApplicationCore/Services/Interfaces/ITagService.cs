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
        Task<bool> CreateTagAsync(CreateTagModel model);
        Task<bool> DeleteTagAsync(Guid tagID);
        Task<bool> CreatePlaceTags(Guid placeID, List<Guid> tagIDs);
        Task<bool> CreateProductTagsAsync(Product product, List<Guid> tagIDs);
        Task<bool> ActivateTagAsync(Guid tagID);
    }
}
