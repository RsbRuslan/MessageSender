using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageSender.Interfaces;
using MessageSender.Models;
using MessageSender.Models.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageSender.Controllers
{
    [Produces("application/json")]
    [Route("api/Message")]
    [EnableCors("AllowOrigin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly INotificationService _notificationService;

        public MessageController(IMessageService messageService, INotificationService notificationService)
        {
            _messageService = messageService;
            _notificationService = notificationService;
        }

        [HttpGet("status/{messageId}")]
        public async Task<Response<MessageResponse>> CheckStatus(string messageId)
        {
            try
            {
                var existing = _messageService.GetMessage(messageId);

                if (existing == null) return Response<MessageResponse>.Fail("message not found");

                return Response<MessageResponse>.Success(new MessageResponse(){ Message = existing, MessageId = messageId});
            }
            catch (Exception e)
            {
                return Response<MessageResponse>.Fail(e.Message);
            }
            

        }

        [HttpPost]
        public async Task<Response<MessageResponse>> SendMessage([FromBody]Message message)
        {
            try
            {
                var result = await _messageService.SendMessage(message);

                var msg = _messageService.GetMessage(result);
                return Response<MessageResponse>.Success(
                    new MessageResponse() {Message = message, MessageId = result});
            }
            catch (Exception e)
            {
                return Response<MessageResponse>.Fail(e.Message);
            }
        }
    }
}