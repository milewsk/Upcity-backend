using ApplicationCore.Services.Interfaces;
using Infrastructure.Data.Dto;
using Infrastructure.Helpers;
using Infrastructure.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PlaceService : IPlaceService
    {
        public async Task<List<PlaceDto>> GetPlacesAsync()
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
        public async Task<Tuple<PlaceCreatePlaceStatusResult, PlaceDto>> CreatePlaceAsync()
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
                //_appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public Task<Tuple<PlaceEditPlaceStatusResult, bool>> EditPlaceAsync()
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
                //_appLogger.LogWarning(ex.Message);
                return null;
            }
        }
    }
}
