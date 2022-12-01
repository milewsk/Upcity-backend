using Common.Dto.Inbox;
using Common.Dto.Models;
using Common.Dto.Place;
using Common.Dto.Product;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IPlaceMenuService
    {
        Task<bool> CreateMenuCategoryAsync(CreatePlaceMenuCategoryModel categoryModel);
        Task<bool> DeleteMenuCategoryAsync(Guid categoryID);
    }
}
