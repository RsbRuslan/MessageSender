using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Models.Responses
{
    public class ResponseBase
    {
        public bool IsTransactionSuccessed { get; set; }

        public string ErrorMessage { get; set; }

        public static ResponseBase Fail(string errorMessage)
        {
            return new ResponseBase()
            {
                IsTransactionSuccessed = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
