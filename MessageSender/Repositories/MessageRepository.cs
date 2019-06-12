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
        private readonly string _dbFilePath;

        public MessageRepository()
        {
            _dbFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _dbFilePath += "/MessagesStorage.db";
        }

        public string SaveMessage(Message message)
        {
            return Invoke<string, MessageDto>((db, messages) =>
            {
                var messageDto = new MessageDto(message);

                messages.Insert(messageDto);

                return messageDto.Id;
            });
        }

        public Message GetMessage(string id) => Invoke<Message, MessageDto>((db, messages) => messages.FindById(id).ToDomain());

        public void UpdateStatus(string id, bool newStatus)
        {
            Invoke<MessageDto>((db, messages) =>
            {
                var existing = messages.FindById(id);

                existing.Status = newStatus;

                messages.Update(existing);
            });
        }

        private void Invoke<TCollection>(Action<LiteDatabase, LiteCollection<TCollection>> action)
        {
            using (var db = new LiteDatabase(_dbFilePath))
            {
                LiteCollection<TCollection> collection = db.GetCollection<TCollection>();

                action.Invoke(db, collection);
            }
        }

        private TReturn Invoke<TReturn, TCollection>(Func<LiteDatabase, LiteCollection<TCollection>, TReturn> action)
        {
            using (var db = new LiteDatabase(_dbFilePath))
            {
                LiteCollection<TCollection> collection = db.GetCollection<TCollection>();

                return action(db, collection);
            }
        }
        
    }
}
