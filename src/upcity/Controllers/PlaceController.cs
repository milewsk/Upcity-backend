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
using System.IdentityModel.Tokens.Jwt;
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

                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    //var handler = new JwtSecurityTokenHandler();
                    //JwtSecurityToken tokenn = handler.ReadJwtToken(jwtHeader);
                    var token = _jwtService.Verify(Convert.ToString(jwtHeader));
                    Guid userID = Guid.Parse(token.Payload.Iss);
                    //Guid userID = Guid.Parse("68a06b43-eb63-46c6-326a-08daa240262c");
                    var result = await _placeService.GetPlaceListNearLocationAsync(latitude, longitude, userID);

                    foreach (var place in result)
                    {
                        place.Image = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, place.Image);
                    }

                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        //duplikacja z tym wyżej
        [Route("places/{latitude}/{longitude}/{tagID}")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceByCategoryAsync([FromRoute] string latitude, [FromRoute] string longitude, [FromRoute] Guid tagID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = _jwtService.Verify(jwtHeader.ToString());
                    Guid userID = Guid.Parse(token.Payload.Iss);
                    var result = await _placeService.GetPlacesByCategoryAsync(latitude, longitude, tagID, userID);

                    foreach (var place in result)
                    {
                        place.Image = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, place.Image);
                    }
                    return Ok(result);
                }

                return BadRequest();
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

                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = _jwtService.Verify(jwtHeader.ToString());
                    Guid userID = Guid.Parse(token.Payload.Iss);
                    //  Guid userID = Guid.Parse("68a06b43-eb63-46c6-326a-08daa240262c");
                    var result = await _placeService.GetPlacesListBySearchStringAsync(searchString, latitude, longitude, userID);

                    foreach (var place in result)
                    {
                        place.Image = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, place.Image);
                    }

                    return Ok(result);
                }

                return BadRequest();
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

        [Route("hours/{placeID}")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceOpeningHours([FromRoute] Guid placeID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                {
                    return Unauthorized();
                }

                var result = await _placeService.GetPlaceOpeningHoursAsync(placeID);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("hours/update")]
        [HttpPost]
        public async Task<IActionResult> UpdatePlaceOpeningHours([FromBody] UpdatePlaceOpeningHourListModel modelList)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                var result = await _placeService.UpdateOpeningHoursAsync(modelList);
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



        [Route("details/{placeID}")]
        [HttpGet]
        public async Task<IActionResult> GetDetails([FromRoute] string placeID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                //we want to get user based on request
                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = _jwtService.Verify(jwtHeader.ToString());
                    Guid userID = Guid.Parse(token.Payload.Iss);
                    var result = await _placeService.GetPlaceDetailsAsync(Guid.Parse(placeID), userID);
                    result.Image = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, result.Image);

                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("favourite/{placeID}")]
        [HttpPost]
        public async Task<IActionResult> AddToFavourite([FromRoute] string placeID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                //we want to get user based on request
                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = _jwtService.Verify(jwtHeader.ToString());
                    Guid userID = Guid.Parse(token.Payload.Iss);
                    //Guid userID = Guid.Parse("68a06b43-eb63-46c6-326a-08daa240262c");
                    var result = await _placeService.AddToFavouriteAsync(Guid.Parse(placeID), userID);
                    return Ok(result);
                }
                return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("places/favourite")]
        [HttpGet]
        public async Task<IActionResult> GetFavouritePlaceList()
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                //we want to get user based on request
                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = _jwtService.Verify(jwtHeader.ToString());
                    Guid userID = Guid.Parse(token.Payload.Iss);
                    //Guid userID = Guid.Parse("68a06b43-eb63-46c6-326a-08daa240262c");
                    var result = await _placeService.GetFavouritePlaceListAsync(userID);

                    foreach (var place in result)
                    {
                        place.Image = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, place.Image);
                    }

                    return Ok(result);
                }
                return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
