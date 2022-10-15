using ApplicationCore.Interfaces;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<Reservation> GetReservationAsync(Guid reservationID);
        Task<List<Reservation>> GetAllActiveUserReservations(Guid userID);
        Task<bool> CreateReservationAsync(Reservation reservation);
    }
}
