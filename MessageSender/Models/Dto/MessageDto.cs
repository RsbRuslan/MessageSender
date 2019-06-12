using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MessageSender.Models.Dto
{
    public class MessageDto
    {
        public string Id { get; set; }

        public string[] Recepients { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool Status { get; set; }

        public MessageDto() { }

        public MessageDto(Message message)
        {
            Id = Guid.NewGuid().ToString();
            Body = message.Body;
            Subject = message.Subject;
            Recepients = message.Recepients;
            Status = message.IsDelivered;
        }

        public Message ToDomain() => new Message()
        {
            Body = Body,
            Subject = Subject,
            Recepients = Recepients,
            IsDelivered = Status
        };
    }
}
