using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upcity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository2 _userRepository;
        private readonly IJwtService _jwtService;

        public UserController(IUserRepository2 userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;

        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                Guid userId = new Guid(token.Issuer);

                var user = _userRepository.GetUserByGuid(userId);

                return Ok(user);

            }catch(Exception e)
            {
                return Unauthorized();
                throw;
            }
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {

            User user = _userRepository.CreateUser(userDto);
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
            var user = _userRepository.LoginUser(userDto);

            if (user == null) return BadRequest(new { message = "Podany email lub hasło są niepoprawne" });

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

            
            return Ok(JsonConvert.SerializeObject(new ResponseSchema( 200,"Logowanie pomyślne", new { jwt })));
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout() {

            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Cookie removed" });
        }

    }
}
