using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Tag;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class TagService : ITagService
    {
        private readonly ILogger<TagService> _appLogger;
        private readonly ITagRepository _tagRepository;
        private readonly IJwtService _jwtService;

        public TagService(ITagRepository tagRepository, ILogger<TagService> appLogger, IJwtService jwtService)
        {
            _tagRepository = tagRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
        }

        public async Task<bool> CreateTagAsync(CreateTagModel model)
        {
            try
            {
                Tag newTag = new Tag()
                {
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    Name = model.Name,
                    Color = model.Color,
                    IsActive = 1,
                };

                var result = await _tagRepository.AddAsync(newTag);

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteTagAsync(Guid tagID)
        {
            try
            {
                var tag = await _tagRepository.GetOne(tagID);

                if (tag != null)
                {
                    tag.LastModificationDate = DateTime.Now;
                    tag.IsActive = 0;

                    await _tagRepository.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreatePlaceTags(Guid placeID, List<Guid> tagIDs)
        {
            try
            {


                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateProductTagsAsync(Product product, List<Guid> tagIDs)
        {
            try
            {
                List<ProductTag> productTagList = new List<ProductTag>();
                var tagList = await _tagRepository.GetListByIDsAsync(tagIDs);

                if (tagList.Count == 0)
                {
                    return false;
                }

                //we asume that every tag here is active
                foreach (var tag in tagList)
                {
                    ProductTag productTag = new ProductTag()
                    {
                        CreationDate = DateTime.Now,
                        LastModificationDate = DateTime.Now,
                        ProductID = product.ID,
                        TagID = tag.ID
                    };

                    productTagList.Add(productTag);
                }

                var result = await _tagRepository.AddProductTagAsync(productTagList);

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> ActivateTagAsync(Guid tagID)
        {
            try
            {
                var tag = await _tagRepository.GetOne(tagID);

                if (tag != null)
                {
                    tag.LastModificationDate = DateTime.Now;
                    tag.IsActive = 1;

                    await _tagRepository.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
