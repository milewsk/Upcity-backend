using ApplicationCore.Services.Interfaces;
using Common.Dto.Models;
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
    public class ProductController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IProductService _productService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public ProductController(IPlaceService userService, IProductService productService, IJwtService jwtService, IAuthorizationService authService)
        {
            _placeService = userService;
            _productService = productService;
            _jwtService = jwtService;
            _authService = authService;
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductModel productModel)
        {
            try
            {
                //if (!await _authService.Authorize(Request, _jwtService, UserClaimsEnum.Owner))
                //{
                //    return Unauthorized();
                //}

                var result = await _productService.CreateProductAsync(productModel);

                if (result == true)
                {
                    return Ok(result);
                }

                return BadRequest(false);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
