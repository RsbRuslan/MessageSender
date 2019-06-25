using MessageSender.Models;

namespace MessageSender.Interfaces
{
    public interface IMessageRepository
    {
        Message GetMessage(string id);
        string SaveMessage(Message message);
        void UpdateMessage(Message message, string id);
    }
}