using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationService.Models;

namespace NotificationService.Interfaces
{
    public interface INotificationService
    {
        void CreateNotification(Notification notification);
        bool IsNotificationExists(Notification notification);
    }
}
