using System;
using System.Threading.Tasks;

namespace MessageSender.Interfaces
{
    public interface IHttpService
    {
        Task<T> Get<T>(string requestUrl);
        Task<T> Post<T>(string requestUrl, object item);
        Task<string> Post(string requestUrl, object item);
    }
}