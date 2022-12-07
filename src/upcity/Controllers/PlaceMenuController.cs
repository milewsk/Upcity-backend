using ApplicationCore.Services.Interfaces;
using Common.Dto.Place;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceMenuController : ControllerBase
    {
        private readonly IPlaceMenuService  _placeMenuService;
        private readonly IProductService _productService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public PlaceMenuController(IPlaceMenuService placeMenuService, IProductService productService, IJwtService jwtService, IAuthorizationService authService)
        {
            _placeMenuService = placeMenuService;
            _productService = productService;
            _jwtService = jwtService;
            _authService = authService;
        }

        [Route("menu/{placeID}")]
        [HttpPost]
        public async Task<IActionResult> GetPlaceMenu([FromRoute] Guid placeID)
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                //{
                //    return Unauthorized();
                //}

                var result = await _placeMenuService.GetPlaceMenuAsync(placeID);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("category/add")]
        [HttpPost]
        public async Task<IActionResult> CreatePlaceMenuCategoryAsync([FromBody] CreatePlaceMenuCategoryModel categoryModel)
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                //{
                //    return Unauthorized();
                //}

                bool result = await _placeMenuService.CreateMenuCategoryAsync(categoryModel);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("category/delete/{categoryID}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePlaceMenuCategory([FromRoute] Guid categoryID)
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                //{
                //    return Unauthorized();
                //}

                bool productResult = await _productService.DeleteCategoryProductListAsync(categoryID);
                bool result = await _placeMenuService.DeleteMenuCategoryAsync(categoryID);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
