using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using upcity.Helpers;
using Common.Helpers;
using Common.Dto;
using Infrastructure.Data.Models;
using System.Net;
using Common.Enums;
using System.Net.Http;
using PublicApi.Controllers;
using Common.Dto.Models;

namespace upcity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public UserController(IUserService userService, IJwtService jwtService, IAuthorizationService authorizationService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _authService = authorizationService;
        }

        //move to service
        [HttpGet]
        [Route("check")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                Guid userId = new Guid(token.Issuer);

                var user = await _userService.GetUserByGuidAsync(userId);

                return Ok(user);

            }
            catch (Exception e)
            {
                return Unauthorized();
                throw;
            }

        }

        [Route("checkEmail/{email}")]
        [HttpGet]
        public async Task<IActionResult> CheckEmail([FromRoute] string email)
        {
            try
            {

                var isEmailAlreadyTaken = await _userService.IsEmailExist(email);
                if (isEmailAlreadyTaken)
                {
                    return Conflict();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserModel userModel)
        {
            var result = await _userService.RegisterUser(userModel);

            if (result.Item2 != null)
            {
                return Created("created", result.Item2);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDto userDto)
        {
            try
            {
                Tuple<UserLoginResult, User> result = await _userService.GetUserByEmailAndPasswordAsync(userDto.Email, userDto.Password);

                if (result != null)
                {
                    switch (result.Item1)
                    {
                        case UserLoginResult.Ok:
                            var jwt = _jwtService.Generate(result.Item2.ID);
                            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

                            var userDetails = await _userService.GetUserDetailsAsync(result.Item2.ID);
                            var userClaim = await _userService.GetUserClaimAsync(result.Item2.ID);
                            if(userDetails == null || userClaim == null)
                            {
                                return BadRequest(new { errorMessage = "404 Error" });
                            }

                            UserLoginDto data = new UserLoginDto()
                            {
                                FirstName = userDetails.FirstName,
                                Jwt = jwt,
                                Claim = userClaim.Value
                            };

                            return Ok(data);
                        case UserLoginResult.WrongPassword:
                            return NotFound(new { errorMessage = result.Item1.GetDescription() });
                        case UserLoginResult.UserNotFound:
                            return NotFound(new { errorMessage = result.Item1.GetDescription() });
                        default:
                            return BadRequest(new { errorMessage = "404 Error" });
                    }

                }
                return BadRequest(UserLoginResult.Error);
            }
            catch (Exception ex)
            {
                return BadRequest(UserLoginResult.Error);
                throw;
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Response.Cookies.Delete("jwt");

                return Ok(new { message = "Cookie removed" });
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("loyalityCard")]
        [HttpGet]
        public async Task<IActionResult> GetLoyalityCardInfo([FromRoute] string email)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                var isEmailAlreadyTaken = await _userService.IsEmailExist(email);
                if (isEmailAlreadyTaken)
                {
                    return Conflict();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
