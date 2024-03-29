﻿using ApplicationCore.Repositories.Interfaces;
using Common.Enums;
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
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessageRepository> _logger;

        public MessageRepository(ApplicationDbContext context, ILogger<MessageRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreatePrivateMessageAsync(PrivateMessage message)
        {
            try
            {
                await _context.PrivateMessages.AddAsync(message);
                await _context.SaveChangesAsync();

                if (message.ID != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        
        public async Task<bool> CreatePlaceMessageAsync(PlaceMessage message)
        {
            try
            {
                await _context.PlaceMessages.AddAsync(message);
                await _context.SaveChangesAsync();

                if (message.ID != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PlaceMessage>> GetPlaceMessagesForUserAsync(List<Guid> placeIDs)
        {
            try
            {
                var result = await _context.PlaceMessages.Include(x=> x.Place).Where(x => placeIDs.Contains(x.PlaceID)).OrderBy(x => x.CreationDate).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<PrivateMessage>> GetPrivateMessagesForUserAsync(Guid userID)
        {
            try
            {
                var result = await _context.PrivateMessages.Where(x => x.UserID == userID).OrderBy(x => x.CreationDate).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
