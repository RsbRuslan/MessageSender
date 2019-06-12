using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Models.Responses
{
    public class MessageResponse
    {
        public string MessageId { get; set; }

        public Message Message { get; set; }
    }
}
