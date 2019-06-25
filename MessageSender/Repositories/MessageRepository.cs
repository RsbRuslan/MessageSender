using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LiteDB;
using MessageSender.Interfaces;
using MessageSender.Models;
using MessageSender.Models.Dto;

namespace MessageSender.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbService<MessageDto> _dbService;

        public MessageRepository(IDbService<MessageDto> dbService)
        {
            _dbService = dbService;
        }

        public Message GetMessage(string id) => _dbService.Invoke<Message>((db, messages) => messages.FindById(id).ToDomain());

        public string SaveMessage(Message message)
        {
            return _dbService.Invoke<string>((db, messages) =>
            {
                var messageDto = new MessageDto(message);

                messages.Insert(messageDto);

                return messageDto.Id;
            });
        }

        public void UpdateMessage(Message message, string id)
        {
            _dbService.Invoke((db, messages) =>
            {
                var dto = new MessageDto(message) {Id = id};

                messages.Update(dto);
            });
        }
    }
}
