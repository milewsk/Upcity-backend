using ApplicationCore.Services.Interfaces;
using Common.Dto.Inbox;
using Common.Enums;
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
    public class InboxController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IProductService _productService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public InboxController(IMessageService messageService, IProductService productService, IJwtService jwtService, IAuthorizationService authService)
        {
            _messageService = messageService;
            _jwtService = jwtService;
            _authService = authService;
        }


        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageModel model)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                var result = await _messageService.CreateMessageAsync(model);
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
        public async Task<IActionResult> CreateAdminMessage([FromBody] CreateMessageAdminModel model)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                {
                    return Unauthorized();
                }

                var result = await _messageService.CreateAdminMessageAsync(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("messages")]
        [HttpGet]
        public async Task<IActionResult> GetUserMessageList()
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

                    var result = await _messageService.GetUserMessagesAsync(userID);
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
