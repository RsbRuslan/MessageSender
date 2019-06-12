using System.Threading.Tasks;

namespace MessageSender.Models.Responses
{
    public class Response<T>
    {
        public bool IsTransactionSuccessed { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }

        public static Response<T> Success(T item)
        {
            return new Response<T>()
            {
                IsTransactionSuccessed = true,
                Data = item
            };
        }

        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T>
            {
                IsTransactionSuccessed = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
