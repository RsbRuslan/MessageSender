using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

        

        [HttpGet("messagestatistics/{messageId}")]
        public async Task<Response<IEnumerable<RecepientStatus>>> MessageStatistics(string messageId)
        {
            var response = _messageService.GetRecepientReceiveStatuses(messageId);

            List<RecepientStatus> statuses = new List<RecepientStatus>();

            foreach (var messageStatuses in response)
            {
                statuses.Add(new RecepientStatus()
                {
                    MessageId = messageId,
                    Status = messageStatuses.DeliverySatus,
                    Recepient = messageStatuses.Recepient
                });
            }

            return Response<IEnumerable<RecepientStatus>>.Success(statuses);


        }

        [HttpPost]
        public async Task<Response<MessageResponse>> SendMessage([FromBody]Message message)
        {
            var result = await _messageService.SendMessage(message);
            return Response<MessageResponse>.Success(
                new MessageResponse() { Message = message, MessageId = result });
        }

        [HttpPost("notification/{messageId}")]
        public async Task<Response<RecepientStatus>> MessageRecepientStatistics([FromRoute]string messageId, [FromBody]RecepientContainer container)
        {
            var response = await _notificationService.IsNotified(container.Recepient, messageId);

            return Response<RecepientStatus>.Success(new RecepientStatus()
            {
                MessageId = messageId,
                Status = response,
                Recepient = container.Recepient
            });


        }
    }
}