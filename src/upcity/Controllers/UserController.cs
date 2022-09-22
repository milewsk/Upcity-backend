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
using Infrastructure.Helpers;
using Infrastructure.Data.Dto;
using Infrastructure.Data.Models;
using System.Net;
using Infrastructure.Helpers.Enums;
using System.Net.Http;


namespace upcity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;

        }

        //move to service
        [HttpGet]
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
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            var result = await _userService.CreateUser(userDto.Email, userDto.Password);
            //edit this
            if (result.Item2 != null)
            {
                //return jwt
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
                Tuple<UserLoginResult, User> result = await _userService.GetUser(userDto.Email, userDto.Password);
            Tuple<UserLoginResult, User> result = await _userService.GetUserByEmailAndPasswordAsync(userDto.Email, userDto.Password);

                if (result != null)
                {
                    switch (result.Item1)
                    {
                        case UserLoginResult.Ok:
                            var jwt = _jwtService.Generate(result.Item2.ID);
                            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
                            return Ok(new { jwt });
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

            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Cookie removed" });
        }

    }
}
