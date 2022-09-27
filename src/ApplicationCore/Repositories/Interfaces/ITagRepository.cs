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
        Task<List<Tag>> GetTagListAsync();
        Task<List<Tag>> GetPlaceTagListAsync(Guid placeID);
        Task<List<Tag>> GetProductTagListAsync(Guid productID);
    }
}
