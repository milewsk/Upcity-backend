using Infrastructure.Data.Models;
using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Enums;
using Infrastructure.Services.Interfaces;
using System.Text.RegularExpressions;
using System.Security.Claims;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IAppLogger<Exception> _appLogger;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IAppLogger<Exception> appLogger, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
        }

        public async Task<Tuple<UserRegisterResult, string>> RegisterUser(string email, string password)
        {
            try
            {
                if (await IsEmailExist(email))
                {
                    return new Tuple<UserRegisterResult, string>(UserRegisterResult.EmailAlreadyTaken, null);
                }

                if (CrudentialsValidator(email, password))
                {
                   // przekażemy typ usera który chcemy stworzyć

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
                        var jwt = _jwtService.Generate(user.ID);
                        return new Tuple<UserRegisterResult, string>(UserRegisterResult.Ok, jwt);
                    }
                    else
                    {
                        return new Tuple<UserRegisterResult, string>(UserRegisterResult.Error, null);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }
        public async Task<Tuple<UserLoginResult, bool>> CreateUserDetails(string jwt)
        {
            var ss = _jwtService.Verify(jwt);
            var user = await GetUserByGuidAsync(Guid.Parse(ss.Payload.Iss));
            if (user.ID != null)

            {

            }
            //LoyalityProgramAccount loyalProgram = new LoyalityProgramAccount()
            //{
            //    CreationDate = DateTime.Now,
            //    LastModificationDate = DateTime.Now,
            //    Points = 0,
            //    UserID = user.ID,
            //    User = user
            //};
            return null;
        }

        public async Task<User> GetUserByGuidAsync(Guid id)
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

        public async Task<Tuple<UserLoginResult, User>> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetUser(email, password);
                if (user == null)
                {
                    return new Tuple<UserLoginResult, User>(UserLoginResult.UserNotFound, null);
                }

                return new Tuple<UserLoginResult, User>(UserLoginResult.Ok, user);
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
                return await _userRepository.IsUserExistWithEmail(email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return false;
            }
        }

        private bool CrudentialsValidator(string email, string password)
        {
            try
            {
                Regex emailRegex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                                + "@"
                                                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
                Regex passwordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

                if (emailRegex.IsMatch(email) && passwordRegex.IsMatch(password))
                {
                    return true;
                }

                //do zmiany potem 
                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return false;
            }
        }

        private async Task<User> CreateUser(string email, string password)
        {
            try
            {
                User user = new User()
                {
                    Email = email,
                    Password = password,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                };

               _userRepository.Add(user);
                return user;

            }
            catch (Exception ex)
            {
                
                return null;
            }

        }

        public  ClaimsPrincipal ConvertUserToClaims(User user)
        {
            var claims = new List<Claim>(){
                new Claim(type: "user",value: user.Email)

            };

            //_userRepository.AddClaims(user, claims);

            var identity = new ClaimsIdentity(claims);
            return new ClaimsPrincipal(identity);
        }
    }
}
