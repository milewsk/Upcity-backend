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
                    Type = model.Type,
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

                await _tagRepository.RemoveTagBoundsAsync(tag);

                if (tag != null)
                {
                    await _tagRepository.Remove(tag);
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

        public async Task<bool> CreatePlaceTagsAsync(Place place, List<Guid> tagIDs)
        {
            try
            {
                List<PlaceTag> productTagList = new List<PlaceTag>();
                var tagList = await _tagRepository.GetListByIDsAsync(tagIDs);

                if (tagList.Count == 0)
                {
                    return false;
                }

                var listIDs = await _tagRepository.FindNotCreatedPlaceTagsAsync(place, tagIDs);


                if (listIDs.Count == 0)
                {
                    return true;
                }

                //we asume that every tag here is active
                foreach (var id in listIDs)
                {
                    PlaceTag productTag = new PlaceTag()
                    {
                        CreationDate = DateTime.Now,
                        LastModificationDate = DateTime.Now,
                        PlaceID = place.ID,
                        TagID = id
                    };

                    productTagList.Add(productTag);
                }

                var result = await _tagRepository.AddPlaceTagAsync(productTagList);

                return result;
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

                var listIDs = await _tagRepository.FindNotCreatedProductTagsAsync(product, tagIDs);


                if (listIDs.Count == 0)
                {
                    return true;
                }

                //we asume that every tag here is active
                foreach (var id in listIDs)
                {
                    ProductTag productTag = new ProductTag()
                    {
                        CreationDate = DateTime.Now,
                        LastModificationDate = DateTime.Now,
                        ProductID = product.ID,
                        TagID = id
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

    }
}
