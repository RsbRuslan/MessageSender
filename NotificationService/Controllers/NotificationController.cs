using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Interfaces;
using NotificationService.Models;

namespace NotificationService.Controllers
{
    [Produces("application/json")]
    [Route("api/Notification")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("status")]
        public bool IsExists(Notification notification)
        {
            return _notificationService.IsNotificationExists(notification);
        }

        [HttpPost("notify")]
        public void NotificationArrived(Notification notification)
        {
            _notificationService.CreateNotification(notification);
        }
    }
}