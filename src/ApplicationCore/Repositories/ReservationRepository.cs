using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(ApplicationDbContext context, ILogger<ReservationRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Reservation>> GetUserReservationListAsync(Guid userID)
        {
            try
            {
                return await _context.Reservations.Where(x => x.UserID == userID).OrderBy(x => x.StartTime).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Reservation>> GetAllActiveUserReservationsAsync(Guid userID)
        {
            try
            {
                return await _context.Reservations.Where(x => x.UserID == userID && x.StartTime > DateTime.Now).OrderBy(x => x.StartTime).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<Reservation> GetReservationAsync(Guid reservationID)
        {
            try
            {
                return await _context.Reservations.Where(x => x.ID == reservationID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            try
            {
                await _context.Reservations.AddAsync(reservation);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

    }
}
