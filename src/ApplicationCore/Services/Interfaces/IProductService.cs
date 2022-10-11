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
        Task<List<ProductResult>> GetProductListAsync(List<PlaceMenuCategory> categiries);

        Task<bool> CreateProductAsync(CreateProductModel productModel);
    }
}
