using ApplicationCore.Services.Interfaces;
using Infrastructure.Data.Dto;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    [Route("api/[controller]")]
    public class PlaceController : ApiControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IJwtService _jwtService;

        public PlaceController(IPlaceService userService, IJwtService jwtService)
        {
            _placeService = userService;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Route("places")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPlacesListAsync() {
            try
            {

                //var result = await _placeService.(email);
               // if (result.c)
             //   {
           //         return Conflict();
           //     }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

    }
}
