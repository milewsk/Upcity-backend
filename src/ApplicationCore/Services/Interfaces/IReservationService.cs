using Common.Dto.Reservation;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IReservationService
    {
        Task<bool> CreateReservationAsync(CreateReservationModel model);

        Task<bool> CancelReservationAsync();

        Task<List<ReservationResult>> GetActiveUserReservationsAsync(Guid userID);
        Task<List<ReservationResult>> GetUserReservationsAsync(Guid userID);
    }
}
