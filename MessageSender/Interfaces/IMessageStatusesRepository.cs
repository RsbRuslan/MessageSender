using System.Collections.Generic;
using MessageSender.Models;

namespace MessageSender.Interfaces
{
    public interface IMessageStatusesRepository
    {
        IEnumerable<MessageStatuses> GetStatusesByMessageId(string id);
        void CreateRecord(MessageStatuses record);
    }
}