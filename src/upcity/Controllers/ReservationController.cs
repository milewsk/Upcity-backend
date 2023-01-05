using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Reservation;
using Common.Enums;
using Infrastructure.Data.Models;
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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IProductService _productService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;
        private IUserRepository _userRepository;

        public ReservationController(IReservationService reservationService, IProductService productService, IJwtService jwtService, IAuthorizationService authService)
        {
            _reservationService = reservationService;
            _jwtService = jwtService;
            _authService = authService;
        }

        [Route("place/{placeID}")]
        [HttpGet]
        public async Task<IActionResult> GetPlaceReservationList([FromRoute] Guid placeID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                {
                    return Unauthorized();
                }

                var result = await _reservationService.GetPlaceReservationListAsync(placeID);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationModel model)
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
                    var result = await _reservationService.CreateReservationAsync(model, userID);
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

        [Route("status/{reservationID}/{status}")]
        [HttpPost]
        public async Task<IActionResult> ChangeReservationStatus([FromRoute] Guid reservationID, ReservationStatus status)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _reservationService.ChangeReservationStatusAsync(reservationID, status);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        //thats for user cancel
        [Route("cancel/{reservationID}")]
        [HttpPost]
        public async Task<IActionResult> CancelReservation([FromRoute] Guid reservationID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _reservationService.CancelReservationAsync(reservationID);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("details/{reservationID}")]
        [HttpGet]
        public async Task<IActionResult> GetDetails([FromRoute] Guid reservationID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }
                var result = await _reservationService.GetReservationDetailsAsync(reservationID);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("reservations")]
        [HttpGet]
        public async Task<IActionResult> GetUserReservationList()
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                var result = await _reservationService.GetUserReservationListAsync(Request, _jwtService);

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
