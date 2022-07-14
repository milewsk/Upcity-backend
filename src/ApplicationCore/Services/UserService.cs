using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    class UserService : IUserService
    {
        private readonly IAppLogger<Exception> _appLogger;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IAppLogger<Exception> appLogger)
        {
            _userRepository = userRepository;
            _appLogger = appLogger;
        }

        public async Task<User> CreateUser(string email, string password)
        {
            try
            {
               _userRepository.Add(new User() { Email = email, Password = password });
                return await _userRepository.GetByEmail(email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }
    }
}
