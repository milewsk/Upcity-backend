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
using Common.Dto.Models;
using Common.Dto;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IAppLogger<UserService> _appLogger;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IAppLogger<UserService> appLogger, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
        }

        public async Task<Tuple<UserRegisterResult, UserLoginDto>> RegisterUser(CreateUserModel userModel)
        {
            try
            {
                UserLoginDto result = new UserLoginDto() { };
                if (await IsEmailExist(userModel.Email))
                {
                    return new Tuple<UserRegisterResult, UserLoginDto>(UserRegisterResult.EmailAlreadyTaken, null);
                }

                if (CrudentialsValidator(userModel.Email, userModel.Password))
                {
                    User user = new User()
                    {
                        Email = userModel.Email,
                        Password = userModel.Password,
                        CreationDate = DateTime.Now,
                        LastModificationDate = DateTime.Now,
                    };

                    await _userRepository.AddAsync(user);

                    if (user.ID != null)
                    {
                        var jwt = _jwtService.Generate(user.ID);
                        UserDetails newUserDetails = new UserDetails()
                        {
                            CreationDate = DateTime.Now,
                            LastModificationDate = DateTime.Now,
                            FirstName = userModel.FirstName,
                            Surname = userModel.Surname,
                            BirthDate = userModel.BirthDate,
                            Image = userModel.Image,
                            UserID = user.ID
                        };
                        await _userRepository.CreateUserDetailsAsync(newUserDetails);

                        UserClaim newUserClaim = new UserClaim()
                        {
                            CreationDate = DateTime.Now,
                            LastModificationDate = DateTime.Now,
                            Value = (int)userModel.Claim,
                            UserID = user.ID,
                        };
                        await _userRepository.CreateUserClaimAsync(newUserClaim);

                        LoyalityProgramAccount newAccount = new LoyalityProgramAccount()
                        {
                            CreationDate = DateTime.Now,
                            LastModificationDate = DateTime.Now,
                            UserID = user.ID,
                            Points = 0,

                        };
                        await _userRepository.CreateUserLoyalityAccountAsync(newAccount);

                        var userDetails = await GetUserDetailsAsync(user.ID);
                        var userClaim = await GetUserClaimAsync(user.ID);
                        if (userDetails == null || userClaim == null)
                        {
                            new Tuple<UserRegisterResult, UserLoginDto>(UserRegisterResult.Error, null);
                        }

                        result = new UserLoginDto()
                        {
                            FirstName = userDetails.FirstName,
                            Jwt = jwt,
                            Claim = userClaim.Value
                        };

                        return new Tuple<UserRegisterResult, UserLoginDto>(UserRegisterResult.Ok, result);
                    }
                    else
                    {
                        return new Tuple<UserRegisterResult, UserLoginDto>(UserRegisterResult.Error, null);
                    }
                }

                return new Tuple<UserRegisterResult, UserLoginDto>(UserRegisterResult.Error, null); ;
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }
        }

        public async Task<Tuple<UserLoginResult, bool>> CreateUserLoyalityProgramAccountAsync(string jwt)
        {
            try
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
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }

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
                throw;
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
                throw;
            }
        }

        public async Task<UserDetails> GetUserDetailsAsync(Guid userID)
        {
            try
            {
                var userDetails = await _userRepository.GetUserDetailsAsync(userID);
                if (userDetails == null)
                {
                    return null;
                }

                return userDetails;
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }
        }

        public async Task<LoyalityProgramAccount> GetUserLoyalityCardAsync(Guid userID)
        {
            try
            {
                var card = await _userRepository.GetUserLoyalityCardAsync(userID);
                if (card == null)
                {
                    return null;
                }

                return card;
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }
        }


        public async Task<UserClaim> GetUserClaimAsync(Guid userID)
        {
            try
            {
                var userClaim = await _userRepository.GetUserClaimAsync(userID);
                if (userClaim == null)
                {
                    return null;
                }

                return userClaim;
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
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
                throw;
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
                throw;
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

                await _userRepository.AddAsync(user);
                return user;

            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }

        }

        private async Task<User> GetLoaylityCardInfo(string email, string password)
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

                await _userRepository.AddAsync(user);
                return user;

            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }

        }

        public ClaimsPrincipal ConvertUserToClaims(User user)
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
