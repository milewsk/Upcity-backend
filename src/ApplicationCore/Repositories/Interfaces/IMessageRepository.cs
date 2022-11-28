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
        Task<bool> CreatePlaceMessageAsync(PlaceMessage message);
        Task<bool> CreatePrivateMessageAsync(PrivateMessage message);
        Task<List<PlaceMessage>> GetPlaceMessagesForUserAsync(List<Guid> placeIDs);
        Task<List<PrivateMessage>> GetPrivateMessagesForUserAsync(Guid userID);
    }
}
