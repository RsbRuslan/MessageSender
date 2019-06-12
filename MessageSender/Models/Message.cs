using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Models
{
    public class Message
    {
        public string[] Recepients { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsDelivered { get; set; }
    }
}
