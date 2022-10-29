﻿using ApplicationCore.Services.Interfaces;
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

        //lokalizacja
        [Route("list/{latitude}/{longitude}")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceListNearLocation([FromRoute] string latitude, [FromRoute] string longitude)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _placeService.GetPlaceListNearLocationAsync(latitude, longitude);

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

        //duplikacja z tym wyżej
        [Route("places/{latitude}/{longitude}/{categoryID}")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceByCategoryAsync([FromRoute] string latitude, [FromRoute] string longitude, [FromRoute] string categoryID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _placeService.GetPlacesByCategoryAsync(latitude, longitude, categoryID);

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

        //done
        [HttpGet]
        [Route("search/{searchString}/{latitude}/{longitude}")]
        public async Task<IActionResult> GetPlaceListBySearchStringAsync([FromRoute] string searchString, [FromRoute] string latitude, [FromRoute] string longitude)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _placeService.GetPlacesListBySearchStringAsync(searchString, latitude, longitude);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("place/create")]
        [HttpPost]
        public async Task<IActionResult> CreatePlaceAsync([FromBody] CreatePlaceModel placeModel)
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                //{
                //    return Unauthorized();
                //}
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
