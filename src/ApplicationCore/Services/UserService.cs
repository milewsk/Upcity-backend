using Infrastructure.Data.Models;
using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helpers;
using Infrastructure.Helpers.Enums;

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

        public async Task<Tuple<UserRegisterResult, string>> CreateUser(string email, string password)
        {
            try
            {
                if (await IsEmailExist(email))
                {
                    return new Tuple<UserRegisterResult, string>(UserRegisterResult.EmailAlreadyTaken, null);
                }

                if (CheckCrudentials(email, password))
                {
                    User user = new User()
                    {
                        Email = email,
                        Password = password,
                        CreationDate = DateTime.Now,
                        LastModificationDate = DateTime.Now,
                    };

                    _userRepository.Add(user);
                    if (user.ID != null)
                    {
                        LoyalityProgramAccount loyalProgram = new LoyalityProgramAccount()
                        {
                            CreationDate = DateTime.Now,
                            LastModificationDate = DateTime.Now,
                            Points = 0,
                            UserID = user.ID,
                            User = user
                        };

                        user.LoyalityProgramAccount
                    }
                    else
                    {
                        return new Tuple<UserRegisterResult, string>(UserRegisterResult.Error, null);
                    }



                    //  user.LoyalityProgramAccount = new LoyalityProgramAccount()
                    // {
                    // };
                }
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

        public async Task<Tuple<UserLoginResult, User>> GetUser(string email, string password)
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

        //  private void CreateHashPassowrd(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{

        //}

        private bool CheckCrudentials(string email, string password)
        {
            return true;
        }
    }
}
