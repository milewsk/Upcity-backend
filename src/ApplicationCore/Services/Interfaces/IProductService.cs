using Common.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(CreateProductModel productModel);
    }
}
