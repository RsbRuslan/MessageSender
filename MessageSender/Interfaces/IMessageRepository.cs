using MessageSender.Models;

namespace MessageSender.Interfaces
{
    public interface IMessageRepository
    {
        string SaveMessage(Message message);
        Message GetMessage(string id);
        void UpdateStatus(string id, bool newStatus);
    }
}