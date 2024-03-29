﻿using ApplicationCore.Repositories.Interfaces;
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

                var messages = await _messageRepository.GetPlaceMessagesForUserAsync(placeList);
                var privateMessages = await _messageRepository.GetPrivateMessagesForUserAsync(userID);
                var result = new List<MessageResult>();

                foreach (var message in messages)
                {
                    var item = MappingHelper.Mapper.Map<MessageResult>(message);
                    result.Add(item);
                }

                foreach (var message in privateMessages)
                {
                    var item = new MessageResult()
                    {
                        Content = message.Content,
                        Date = message.Date,
                        PlaceName = "System Message",
                        Title = message.Title,

                    };

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

        public async Task<bool> CreatePlaceMessageAsync(CreateMessageModel model)
        {
            try
            {
                PlaceMessage newMessage = new PlaceMessage()
                {
                    Content = model.Content,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    PlaceID = model.PlaceID,
                    Date = model.Date,
                    Title = model.Title
                };

                return await _messageRepository.CreatePlaceMessageAsync(newMessage);
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreatePrivateMessageAsync(CreateMessageAdminModel model)
        {
            try
            {
                PrivateMessage newMessage = new PrivateMessage()
                {
                    Content = model.Content,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    UserID = model.UserID,
                    Date = model.Date,
                    Title = model.Title
                };

                return await _messageRepository.CreatePrivateMessageAsync(newMessage);
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
