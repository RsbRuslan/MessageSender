using System.Threading.Tasks;

namespace MessageSender.Models.Responses
{
    public class Response<T> : ResponseBase
    {
        public T Data { get; set; }

        public static Response<T> Success(T item)
        {
            return new Response<T>()
            {
                IsTransactionSuccessed = true,
                Data = item
            };
        }
    }
}
