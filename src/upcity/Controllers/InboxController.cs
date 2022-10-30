using ApplicationCore.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    public class InboxController : ControllerBase
    {
        private readonly I _reservationService;
        private readonly IProductService _productService;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authService;

        public InboxController(IReservationService reservationService, IProductService productService, IJwtService jwtService, IAuthorizationService authService)
        {
            _reservationService = reservationService;
            _jwtService = jwtService;
            _authService = authService;
        }

    }
}
