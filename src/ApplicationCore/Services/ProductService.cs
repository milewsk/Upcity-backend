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
                    PlaceMenuCategoryID = productModel.CategoryID,
                    HaveDiscount = false,
                    DiscountPrice = productModel.Price,
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

        public async Task<bool> DeleteCategoryProductListAsync(Guid categoryID)
        {
            try
            {
                var productListToDelete = await _productRepository.GetProductListByMenuCategoryAsync(categoryID);

                if (productListToDelete == null)
                {
                    return false;
                }

                _productRepository.RemoveRange(productListToDelete);
                return true;
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
                var result = await _productRepository.SetDiscountAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        // we are assuming in discount price
        public async Task<bool> EditProductAsync(EditProductModel model)
        {
            try
            {
                var productToEdit = await _productRepository.GetOne(model.ProductID);
                if (productToEdit == null)
                {
                    return false;
                }

                MappingHelper.Mapper.Map(model, productToEdit);

                var result = await _productRepository.EditProductAsync(productToEdit);
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
