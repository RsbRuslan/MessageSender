using System;
using System.Threading.Tasks;

namespace MessageSender.Interfaces
{
    public interface IHttpService
    {
        Task<T> Get<T>(string requestUrl);
        Task Post(string requestUrl, object item);
    }
}