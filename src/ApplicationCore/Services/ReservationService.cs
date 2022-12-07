using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Reservation;
using Common.Enums;
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

        public async Task<bool> CreateReservationAsync(CreateReservationModel model, Guid userID)
        {
            try
            {
                Reservation newReservation = new Reservation()
                {
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    UserID = userID,
                    PlaceID = model.PlaceID,
                    SeatNumber = model.SeatNumber,
                    Price = model.Price,
                    PaymentStatus = PaymentStatus.UnPaid,
                    StartTime = DateTime.Parse(model.StartTime),
                    EndTime = DateTime.Parse(model.EndTime),
                    Status = ReservationStatus.Pending,
                };

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
                reservation.Status = ReservationStatus.Canceled;
                reservation.LastModificationDate = DateTime.Now;

                await _reservationRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> ChangeReservationStatusAsync(Guid reservationID, ReservationStatus status)
        {
            try
            {
                var reservation = await _reservationRepository.GetReservationAsync(reservationID);
                reservation.Status = status;

                await _reservationRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<ReservationShortcutResult>> GetPlaceReservationListAsync(Guid placeID)
        {
            try
            {
                List<ReservationShortcutResult> result = new List<ReservationShortcutResult>();

                List<Reservation> reservations = await _reservationRepository.GetPlaceReservationListAsync(placeID);
                if (reservations != null)
                {
                    foreach (var reservation in reservations)
                    {
                        ReservationShortcutResult item = new ReservationShortcutResult()
                        {
                            ReservationID = reservation.ID,
                            ReservationDate = reservation.StartTime.Date.ToShortDateString(),
                            StartTime = reservation.StartTime.Date.ToShortDateString() + ' ' + reservation.StartTime.ToShortTimeString(),
                            SeatNumber = reservation.SeatNumber,
                            Status = reservation.Status,
                            PaymentStatus = reservation.PaymentStatus,
                            PlaceName = reservation.Place.Name,
                            Price = reservation.Price
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
                    if (reservations != null)
                    {
                        foreach (var reservation in reservations)
                        {
                            ReservationShortcutResult item = new ReservationShortcutResult()
                            {
                                ReservationID = reservation.ID,
                                ReservationDate = reservation.StartTime.Date.ToShortDateString(),
                                StartTime = reservation.StartTime.Date.ToShortDateString() + ' ' + reservation.StartTime.ToShortTimeString(),
                                SeatNumber = reservation.SeatNumber,
                                Status = reservation.Status,
                                PaymentStatus = reservation.PaymentStatus,
                                PlaceName = reservation.Place.Name,
                                Price = reservation.Price
                            };

                            result.Add(item);
                        }
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
                    Status = reservation.Status
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
