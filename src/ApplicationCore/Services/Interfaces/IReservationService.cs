using Common.Dto.Reservation;
using Common.Enums;
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
        Task<List<ReservationShortcutResult>> GetPlaceReservationListAsync(Guid placeID);
        Task<List<ReservationShortcutResult>> GetUserReservationListAsync(HttpRequest request, IJwtService jwtSerivce);
        Task<ReservationResult> GetReservationDetailsAsync(Guid reservationID);
        Task<bool> ChangeReservationStatusAsync(Guid reservationID, ReservationStatus status);
    }
}
