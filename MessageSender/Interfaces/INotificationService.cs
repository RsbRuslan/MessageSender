using System.Threading.Tasks;

namespace MessageSender.Interfaces
{
    public interface INotificationService
    {
        Task<bool> Notify(string recepient, string message);
        Task<bool> IsNotified(string recepient, string message);
    }
}