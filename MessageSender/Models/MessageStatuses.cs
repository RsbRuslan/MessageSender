using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Models
{
    public class MessageStatuses
    {
        public string MessageId { get; set; }

        public string Recepient { get; set; }

        public bool DeliverySatus { get; set; }
    }
}
