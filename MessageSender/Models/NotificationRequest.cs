using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Models
{
    public class NotificationRequest
    {
        public string Recepient { get; set; }
        public string Body { get; set; }
    }
}
