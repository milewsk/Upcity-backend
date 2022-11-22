using ApplicationCore.Interfaces;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<bool> CreateMessageAsync(Message message);
        Task<List<Message>> GetMessagesForUserAsync(List<Guid> placeIDs);
    }
}
