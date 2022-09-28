using Common.Enums;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize(HttpRequest request, IJwtService jwtSerivce, UserClaimsEnum requiredClaim);
    }
}
