using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageSender.Models.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MessageSender.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 200;

                context.Response.ContentType = "application/json";

                string jsonString = JsonConvert.SerializeObject(ResponseBase.Fail(e.Message));

                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
                
                return;
            }
        }
    }
}
