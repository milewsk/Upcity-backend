using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Reservation;
using Infrastructure.Data.Models;
using Infrastructure.Helpers;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> _appLogger;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;

        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository, ILogger<ReservationService> appLogger)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _appLogger = appLogger;
        }

        public async Task<bool> CreateReservationAsync(CreateReservationModel model)
        {
            try
            {
                Reservation newReservation = MappingHelper.Mapper.Map<CreateReservationModel, Reservation>(model);

                await _reservationRepository.CreateReservationAsync(newReservation);

                if (newReservation.ID == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CancelReservationAsync(Guid reservationID)
        {
            try
            {
                var reservation = await _reservationRepository.GetOne(reservationID);
                await _reservationRepository.Remove(reservation);

                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<ReservationResult>> GetActiveUserReservationsAsync(Guid userID)
        {
            try
            {
                List<Reservation> result = await _reservationRepository.GetAllActiveUserReservationsAsync(userID);
                List<ReservationResult> reservationResults = MappingHelper.Mapper.Map<List<Reservation>, List<ReservationResult>>(result);

                return reservationResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
        
        public async Task<List<ReservationShortcutResult>> GetUserReservationListAsync(HttpRequest request, IJwtService jwtSerivce)
        {
            try
            {
                List<ReservationShortcutResult> result = new List<ReservationShortcutResult>();

                //we want to get user based on request
                if (request.Headers.TryGetValue("jwt", out var jwtHeader))
                {
                    var token = jwtSerivce.Verify(jwtHeader.ToString());
                    Guid parsedGuid = Guid.Parse(token.Payload.Iss);
                    User user = await _userRepository.GetUserByGuid(parsedGuid);

                    List<Reservation> reservations = await _reservationRepository.GetUserReservationListAsync(user.ID);
                    foreach(var reservation in reservations)
                    {
                        ReservationShortcutResult item = new ReservationShortcutResult()
                        {
                            ReservationID = reservation.ID,
                            ReservationDate = reservation.StartTime.Date.ToShortDateString(),
                            StartHour = reservation.StartTime.Date.ToShortTimeString(),
                            SeatCount = reservation.SeatNumber,
                            IsActive = DateTime.Now >= reservation.StartTime.Date ? 0 : 1,
                            PlaceName = reservation.Place.Name
                        };

                        result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ReservationResult> GetReservationDetailsAsync(Guid reservationID)
        {
            try
            {
                Reservation reservation = await _reservationRepository.GetReservationAsync(reservationID);
                ReservationResult result = new ReservationResult()
                {
                    PlaceID = reservation.PlaceID,
                    PlaceName = reservation.Place.Name,
                    SeatsCount = reservation.SeatNumber,
                    EndTime = reservation.EndTime.Date.ToShortTimeString(),
                    StartTime = reservation.StartTime.Date.ToShortTimeString(),
                    IsActive = reservation.StartTime > DateTime.Now
                };

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
