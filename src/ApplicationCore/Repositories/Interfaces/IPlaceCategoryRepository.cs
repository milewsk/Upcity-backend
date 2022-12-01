using ApplicationCore.Interfaces;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface IPlaceCategoryRepository : IRepository<PlaceMenuCategory>
    {
        Task<bool> RemoveCategoryAsync(Guid categoryID);
        Task<bool> CreateCategoryAsync(PlaceMenuCategory category);
    }
}
