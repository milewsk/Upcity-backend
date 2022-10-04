using ApplicationCore.Services.Interfaces;
using Common.Dto;
using Common.Dto.Models;
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
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public PlaceController(IPlaceService userService, IJwtService jwtService, IAuthorizationService authService)
        {
            _placeService = userService;
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
                return Ok(result);
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
                var result = _placeService;

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
    }
}
