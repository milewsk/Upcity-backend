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
        Task<bool> CreateMessageAsync(CreateMessageModel model);
        Task<bool> CreateAdminMessageAsync(CreateMessageAdminModel model);
    }
}
