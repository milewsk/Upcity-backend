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

                var user = await _userService.GetUserByGuid(userId);

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

               // UserDto userDto= new UserDto() { Email = "dsad", Password = "dsadasd" };
               // User user = new User() { Email = "dsad", Password = "dsadasd" };

               //var user1 = MappingHelper.Mapper.Map<UserDto>(user);
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

            User user = await _userService.CreateUser(userDto.Email, userDto.Password);
            if (user != null)
            { 
            return Created("created",JsonConvert.SerializeObject(new ResponseSchema(200, "Rejestracja pomyślna", new { email = user.Email })));
            }

            return BadRequest(new ResponseSchema(400, "Taki użytkownik już istnieje", null));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDto userDto)
        {
            User user = await _userService.GetUser(userDto.Email, userDto.Password);
            if(user.ID != null)
            {
                var jwt = _jwtService.Generate(user.ID);
                Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
                return Ok()
            }
            
            return BadRequest(new { message = "Podany email lub hasło są niepoprawne" });

        

         

            return Ok(JsonConvert.SerializeObject(new ResponseSchema(200, "Logowanie pomyślne", new { })));
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
