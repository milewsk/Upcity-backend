using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Models;
using Common.Dto.Product;
using Infrastructure.Data.Models;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _appLogger;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> appLogger)
        {
            _productRepository = productRepository;
            _appLogger = appLogger;
        }

        // query for every 
        public async Task<List<ProductResult>> GetProductListForCategoryAsync(PlaceMenuCategory category)
        {
            try
            {
                List<Product> productList = await _productRepository.GetProductListByMenuCategoryAsync(category.ID);
                List<ProductResult> productResults = MappingHelper.Mapper.Map<List<Product>, List<ProductResult>>(productList);

                return productResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        //done
        public async Task<bool> CreateProductAsync(CreateProductModel productModel)
        {
            try
            {
                Product newProduct = new Product()
                {
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    Name = productModel.Name,
                    Price = productModel.Price,
                    Description = productModel.Description,
                };

                var result = await _productRepository.CreateProductAsync(newProduct);

                if (newProduct.ID == null)
                {
                    return false;
                }

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        //done
        public async Task<bool> DeleteProduct(Guid productID)
        {
            try
            {
                var productToDelete = await _productRepository.GetOne(productID);
                
                if (productToDelete.ID == null)
                {
                    return false;
                }

                var result = await _productRepository.Remove(productToDelete);
                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }  
        
        public async Task<bool> SetDiscountAsync(ProductSetDiscountModel model)
        {
            try
            {
                var productToEdit = await _productRepository.GetOne(model.ProductID);
                

                var result = await _productRepository.SetDiscountAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
