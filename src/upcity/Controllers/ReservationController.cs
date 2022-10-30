﻿using ApplicationCore.Repositories.Interfaces;
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
                var result = await _reservationService.CreateReservationAsync(model);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

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
                var result = await _reservationService.GetDetails(reservationID);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("/reservations")]
        [HttpGet]
        public async Task<IActionResult> GetUserReservationList()
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                List<ReservationShortcutResult> result  = new List<ReservationShortcutResult>();

                //we want to get user based on request
                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = _jwtService.Verify(jwtHeader.ToString());
                    Guid parsedGuid = Guid.Parse(token.Payload.Iss);

                    User user = await _userRepository.GetUserByGuid(parsedGuid);
                     result = await _reservationService.GetUserReservationListAsync(Request, _jwtService);
                }

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
