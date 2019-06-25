using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MessageSender.Interfaces;
using MessageSender.Models;
using MessageSender.Models.Option;
using Microsoft.Extensions.Options;

namespace MessageSender.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHttpService _httpService;
        private readonly NotificationServiceConfig _config;

        public NotificationService(IHttpService httpService, IOptions<NotificationServiceConfig> config)
        {
            _httpService = httpService;
            _config = config.Value;
        }

        public async Task<bool> Notify(string recepient, string message)
        {
            try
            {
                await _httpService.Post($"{_config.ServiceUrl}/notify",
                    new NotificationRequest() {Recepient = recepient, Body = message});
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> IsNotified(string recepient, string message)
        {
            return await _httpService.Post<bool>($"{_config.ServiceUrl}/status",
                new NotificationRequest() { Recepient = recepient, Body = message });
        }
    }
}
