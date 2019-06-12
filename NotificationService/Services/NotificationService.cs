using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationService.Interfaces;
using NotificationService.Models;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IList<Notification> _notifications;

        public NotificationService()
        {
            _notifications = new List<Notification>();
        }

        public void CreateNotification(Notification notification) => _notifications.Add(notification);

        public bool IsNotificationExists(Notification notification) => _notifications.Any(_ =>
            _.Body == notification.Body && _.Recepient == notification.Recepient);
    }
}
