using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Common.Dto.Inbox;
using Common.Dto.Tag;
using Infrastructure.Data.Models;
using Infrastructure.Helpers;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ApplicationCore.Services.Interfaces
{
   public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _appLogger;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserLikeRepository _userLikeRepository;
        private readonly IJwtService _jwtService;

        public MessageService(IMessageRepository messageRepository, IUserLikeRepository userLikeRepository, ILogger<MessageService> appLogger, IJwtService jwtService)
        {
            _messageRepository = messageRepository;
            _userLikeRepository = userLikeRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
        }

        public async Task<List<MessageResult>> GetUserMessagesAsync(Guid userID)
        {
            try
            {
                List<Guid> placeList = await _userLikeRepository.GetPlaceIDsAsync(userID);

                var messages = await _messageRepository.GetMessagesForUserAsync(placeList);
                var result = new List<MessageResult>();

                foreach(var message in messages)
                {
                    var item = MappingHelper.Mapper.Map<MessageResult>(message);
                    result.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateMessageAsync(CreateMessageModel model)
        {
            try
            {
                Message newMessage = new Message()
                {
                    Content = model.Content,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    PlaceID = model.PlaceID,
                    Title = model.Title
                };

                return await _messageRepository.CreateMessageAsync(newMessage);
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
