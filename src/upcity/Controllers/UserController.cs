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
using Common.Dto.User;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace upcity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPlaceService _placeService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserController(IUserService userService, IPlaceService placeService, IJwtService jwtService,
            IAuthorizationService authorizationService, IWebHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _placeService = placeService;
            _jwtService = jwtService;
            _authService = authorizationService;
            _hostEnvironment = hostEnvironment;
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
        public async Task<IActionResult> RegisterUser([FromForm] CreateUserModel userModel)
        {
            var result = await _userService.RegisterUser(userModel, _hostEnvironment);

            if (result.Item2 != null)
            {
                return Created("created", result.Item2);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("register/Owner")]
        public async Task<IActionResult> RegisterOwner([FromForm] CreateOwnerWithPlaceModel model)
        {
            var result = await _userService.RegisterUser(model, _hostEnvironment);

            var resultPlace = await _placeService.CreateOwnerPlaceAsync(model, _hostEnvironment);

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

                            UserLoginDto data = new UserLoginDto();

                            if (userClaim.Value == (int)UserClaimsEnum.Owner)
                            {
                                var placeID = await _placeService.GetOwnerPlaceIDAsync(result.Item2.ID);
                                data.OwnerPlaceID = placeID;
                            }

                            data.FirstName = userDetails.FirstName;
                            data.Jwt = jwt;
                            data.Claim = userClaim.Value;
                            data.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}",Request.Scheme, Request.Host, Request.PathBase, userDetails.Image);

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
        public async Task<IActionResult> GetLoyalityCard()
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("password/{newPassword}")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromRoute] string newPassword)
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
                    var result = await _userService.ChangePasswordAsync(userID, newPassword);

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

        [Route("list/{searchString?}")]
        [HttpGet]
        public async Task<IActionResult> GetUserList([FromRoute] string searchString = "")
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Admin))
                //{
                //    return Unauthorized();
                //}


                if (Request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var result = await _userService.GetUserListAsync(searchString);

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

        [Route("delete/{userID}")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Admin))
                {
                    return Unauthorized();
                }

                var result = await _userService.DeleteUserAsync(userID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        //not gonna use for now
        [Route("edit")]
        [HttpPost]
        public async Task<IActionResult> EditPersonalInfo([FromBody] UserEditModel model)
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
                    //          var result = await _userService.EditUserAsync(userID, model);

                    return Ok(true);
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
