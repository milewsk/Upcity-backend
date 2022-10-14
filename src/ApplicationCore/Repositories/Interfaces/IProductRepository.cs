using ApplicationCore.Interfaces;
using Common.Dto.Models;
using Common.Dto.Product;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetPlaceProductListAsync(Guid placeID);
        Task<List<Product>> GetProductListByMenuCategoryAsync(Guid categoryID);
        Task<bool> RemoveProductAsync(Guid productID);
        Task<bool> CreateProductAsync(Product model);
        //ten discount średnio potrzebny
        Task<bool> SetDiscountAsync(ProductSetDiscountModel model);
        Task<bool> EditProductAsync(Product product);
    }
}
