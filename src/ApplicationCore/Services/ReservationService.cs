using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Reservation;
using Infrastructure.Data.Models;
using Infrastructure.Helpers;
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

        public ReservationService(IReservationRepository reservationRepository, ILogger<ReservationService> appLogger)
        {
            _reservationRepository = reservationRepository;
            _appLogger = appLogger;
        }

        public async Task<bool> CreateReservationAsync(CreateReservationModel model)
        {
            try
            {
                Reservation newReservation = MappingHelper.Mapper.Map<CreateReservationModel, Reservation>(model);


                var result = await _reservationRepository.CreateReservationAsync();

                return placeResults;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
