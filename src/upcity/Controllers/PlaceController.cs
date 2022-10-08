using ApplicationCore.Services.Interfaces;
using Common.Dto;
using Common.Dto.Models;
using Common.Dto.Place;
using Common.Enums;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IProductService _productService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public PlaceController(IPlaceService userService, IProductService productService, IJwtService jwtService, IAuthorizationService authService)
        {
            _placeService = userService;
            _productService = productService;
            _jwtService = jwtService;
            _authService = authService;
        }

        [HttpGet]
        [Route("places")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPlacesListAsync()
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                var result = await _placeService.GetPlacesAsync();
                if (result.Count != 0)
                {
                    return Ok(result);
                }

                return BadRequest(new { errorMessage = "Nie znaleziono żadnych miejsc" });
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        //jeśli udostępnił swoją lokalizację
        [Route("places/{latitude}/{longitude}")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceListUserLocationAsync([FromRoute] string latitude, [FromRoute] string longitude)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _placeService.GetPlacesNearUserLocationAsync(latitude, longitude);

                if (result.Count > 0)
                {
                    return Ok(result);
                }

                return BadRequest(new { errorMessage = "Nie można pobrać danych" });
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("places/search/{searchString}")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceListBySearchStringAsync([FromRoute] string searchString)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = _placeService.GetPlacesListBySearchStringAsync(searchString);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("place/create")]
        [HttpGet]
        public async Task<IActionResult> CreatePlaceAsync([FromBody] CreatePlaceModel placeModel)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _placeService.CreatePlaceAsync(placeModel);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("place/{placeID}/menu")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceMenuAsync([FromRoute] string placeID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                PlaceMenuResult result = await _placeService.GetPlaceMenuResultAsync(Guid.Parse(placeID));

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("place/menu/product/add")]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductModel productModel)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                {
                    return Unauthorized();
                }

                var result = await _productService.CreateProductAsync(productModel);

                if (result == true)
                {
                    return Ok(result);
                }

                return BadRequest(false);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("place/menu/category/add")]
        [HttpPost]
        public async Task<IActionResult> CreatePlaceMenuCategoryAsync([FromBody] CreatePlaceMenuCategoryModel categoryModel)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                {
                    return Unauthorized();
                }

                bool result = await _placeService.CreatePlaceMenuCategoryAsync(categoryModel);

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
