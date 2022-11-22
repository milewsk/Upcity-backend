using Common.Dto.Models;
using Common.Dto.Product;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IProductService
    {
        //seems done
        Task<List<ProductResult>> GetProductListForCategoryAsync(PlaceMenuCategory category);
        Task<bool> CreateProductAsync(CreateProductModel productModel);
        Task<bool> DeleteProduct(Guid productID);
        Task<bool> SetDiscountAsync(ProductSetDiscountModel model);
    }
}
