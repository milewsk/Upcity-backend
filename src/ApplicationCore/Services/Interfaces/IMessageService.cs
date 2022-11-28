using Common.Dto.Inbox;
using Common.Dto.Models;
using Common.Dto.Product;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
   public interface IMessageService
    {
        Task<List<MessageResult>> GetUserMessagesAsync(Guid userID);
        Task<bool> CreatePlaceMessageAsync(CreateMessageModel model);
        Task<bool> CreatePrivateMessageAsync(CreateMessageAdminModel model);
    }
}
