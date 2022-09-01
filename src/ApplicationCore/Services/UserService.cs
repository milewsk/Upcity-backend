using Infrastructure.Data.Models;
using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
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
                //  _userRepository.Add();
                //  return await _userRepository.GetUserByEmail(email);
                return null;
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByGuid(Guid id)
        {
            try
            {
                return await _userRepository.GetUserByGuid(id);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUser(string email, string password)
        {
            try
            {
                return await _userRepository.GetUser(email, password);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<bool> IsEmailExist(string email)
        {
            try
            {
                return await _userRepository.IsEmailExist(email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return false;
            }
        }
    }
}
