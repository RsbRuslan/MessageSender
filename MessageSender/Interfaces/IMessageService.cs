using System.Threading.Tasks;
using MessageSender.Models;

namespace MessageSender.Interfaces
{
    public interface IMessageService
    {
        Task<string> SendMessage(Message message);
        Message GetMessage(string id);
    }
}