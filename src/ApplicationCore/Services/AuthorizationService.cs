
using ApplicationCore.Repositories.Interfaces;
using Common.Enums;
using Infrastructure.Data.Models;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthorizationService(IUserRepository userRepository, ILogger<AuthorizationService> appLogger, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _logger = appLogger;
        }

        public async Task<bool> Authorize(HttpRequest request, JwtService jwtSerivce, UserClaimsEnum requiredClaim)
        {
            try
            {
                if (request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = jwtSerivce.Verify(jwtHeader.ToString());
                    Guid parsedGuid = Guid.Parse(token.Payload.Iss);

                    User user = await _userRepository.GetUserByGuid(parsedGuid);
                    if (user == null)
                    {
                        return false;
                    }

                    UserClaim claim = await _userRepository.GetUserClaimAsync(user, requiredClaim);
                    if (claim == null)
                    {
                        return false;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
