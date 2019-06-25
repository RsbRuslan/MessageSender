using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Models.Dto
{
    public class MessageStatusesDto
    {
        public string Id { get; set; }

        public string MessageId { get; set; }

        public string Recepient { get; set; }

        public bool DeliverySatus { get; set; }

        public MessageStatusesDto() { }

        public MessageStatusesDto(MessageStatuses messageStatus)
        {
            Id = Guid.NewGuid().ToString();
            MessageId = messageStatus.MessageId;
            Recepient = messageStatus.Recepient;
            DeliverySatus = messageStatus.DeliverySatus;
        }

        public MessageStatuses ToDomain() => new MessageStatuses()
        {
            MessageId = MessageId,
            DeliverySatus = DeliverySatus,
            Recepient = Recepient
        };
    }
}
