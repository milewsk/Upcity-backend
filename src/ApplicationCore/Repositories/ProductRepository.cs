using ApplicationCore.Repositories.Interfaces;
using Common.Dto.Models;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Product>> GetPlaceProductListAsync(Guid placeID)
        {
            try
            {
                var query = await (from place in _context.Places
                             join placeMenu in _context.PlaceMenus on place.ID equals placeMenu.PlaceID
                             join placeMenuCategory in _context.PlaceMenuCategories on placeMenu.ID equals placeMenuCategory.PlaceMenuID
                             join product in _context.Products on placeMenuCategory.ID equals product.PlaceMenuCategoryID
                             where place.ID == placeID
                             select product).ToListAsync();
                                                      
                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        } 

        public async Task<List<Product>> GetProductListByMenuCategoryAsync(Guid categoryID)
        {
            try
            {
                List<Product> query = await _context.Products.Where(x => x.PlaceMenuCategoryID == categoryID).ToListAsync();
                                                      
                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> RemoveProductAsync(Guid productID)
        {
            try
            {
                Product product = await _context.Products.Where(x => x.ID == productID).FirstOrDefaultAsync();

                if(product == null)
                {
                    return false;
                }

                _context.Remove<Product>(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateProductAsync(CreateProductModel model)
        {
            try
            {
                if(model == null)
                {
                    return false;
                }

                Product newProduct = new Product()
                {
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                };

                var product = await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();


                if (product == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
