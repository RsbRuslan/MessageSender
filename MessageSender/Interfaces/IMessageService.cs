using System.Collections.Generic;
using System.Threading.Tasks;
using MessageSender.Models;

namespace MessageSender.Interfaces
{
    public interface IMessageService
    {
        Task<string> SendMessage(Message message);
        IEnumerable<MessageStatuses> GetRecepientReceiveStatuses(string messageId);
        Message GetMessage(string id);
    }
}