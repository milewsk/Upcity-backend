using Common.Dto.Reservation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IReservationService
    {
        Task<bool> CreateReservationAsync();

        Task<bool> CancelReservationAsync();

        Task<List<ReservationResult>> GetUserReservationsAsync(Guid userID);
    }
}
