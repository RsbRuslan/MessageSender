using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MessageSender.Interfaces;
using MessageSender.Models;

namespace MessageSender.Services
{
    public class MessageService : IMessageService
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpService _httpService;
        private readonly IMessageRepository _messageRepository;

        public MessageService(INotificationService notificationService, IMessageRepository messageRepository, IHttpService httpService)
        {
            _notificationService = notificationService;
            _messageRepository = messageRepository;
            _httpService = httpService;
        }

        public Message GetMessage(string id)
        {
            return _messageRepository.GetMessage(id);
        }

        public async Task<string> SendMessage(Message message)
        {
            foreach (var recepient in message.Recepients)
                await _httpService.Post($"{recepient}", new
                {
                    message.Subject,
                    message.Body
                });

            var messageId = _messageRepository.SaveMessage(message);

            StartNotifications(message, messageId);

            return messageId;
        }

        private void StartNotifications(Message message, string id)
        {
            Task.Run(async () =>
            {
                Thread.CurrentThread.IsBackground = false;

                bool notificationStatus = true;

                foreach (var recepient in message.Recepients)
                {
                    if (!await _notificationService.Notify(recepient, message.Body))
                        notificationStatus = false;
                }

                if (notificationStatus) _messageRepository.UpdateStatus(id, true);
            });
        }
    }
}
