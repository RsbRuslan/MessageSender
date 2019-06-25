using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageSender.Interfaces;
using MessageSender.Models;
using MessageSender.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MessageSender.Repositories
{
    public class MessageStatusesRepository : IMessageStatusesRepository
    {
        private readonly IDbService<MessageStatusesDto> _dbService;

        public MessageStatusesRepository(IDbService<MessageStatusesDto> dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<MessageStatuses> GetStatusesByMessageId(string id) =>
            _dbService.Invoke<IEnumerable<MessageStatuses>>((db, messages) => messages.Find(_ => _.MessageId.Equals(id)).Select(_ => _.ToDomain()));

        public void CreateRecord(MessageStatuses record)
        {
            _dbService.Invoke((db, records) =>
            { 
                var dto = new MessageStatusesDto(record);

                records.Insert(dto);
            });
        }
    }
}
