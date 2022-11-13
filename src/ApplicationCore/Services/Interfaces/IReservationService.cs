using Common.Dto.Reservation;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IReservationService
    {
        Task<bool> CreateReservationAsync(CreateReservationModel model, Guid userID);
        Task<bool> CancelReservationAsync(Guid reservationID);
        Task<List<ReservationResult>> GetActiveUserReservationsAsync(Guid userID);
        Task<List<ReservationShortcutResult>> GetUserReservationListAsync(HttpRequest request, IJwtService jwtSerivce);
        Task<ReservationResult> GetReservationDetailsAsync(Guid reservationID);

    }
}
