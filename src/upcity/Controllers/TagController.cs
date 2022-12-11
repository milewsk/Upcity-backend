using ApplicationCore.Services.Interfaces;
using Common.Dto.Tag;
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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public TagController(ITagService tagService, IJwtService jwtService, IAuthorizationService authService)
        {
            _tagService = tagService;
            _jwtService = jwtService;
            _authService = authService;
        }
        
        // admin/owner
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> PlaceTagList()
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.User))
                //{
                //    return Unauthorized();
                //}

                List<TagResult> result = await _tagService.GetPlaceTagListAsync();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        //  admin
        [Route("createTag")]
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagModel tagModel)
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                //{
                //    return Unauthorized();
                //}

                bool result = await _tagService.CreateTagAsync(tagModel);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        //  admin
        // i want to implement that but i don't know if i would have time
        //usuń rekordy placeTag/productTag po dezaktywacji Taga
        [Route("delete/{tagID}")]
        [HttpPost]
        public async Task<IActionResult> DeleteTag([FromRoute] string tagID)
        {
            try
            {
                if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                {
                    return Unauthorized();
                }

                Guid tagGuid = Guid.Parse(tagID);

                bool result = await _tagService.DeleteTagAsync(tagGuid);

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
