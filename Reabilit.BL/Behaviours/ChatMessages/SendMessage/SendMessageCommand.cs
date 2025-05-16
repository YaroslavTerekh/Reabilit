using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.SendMessage;

public class SendMessageCommand : IRequest
{
    [JsonIgnore]
    public Guid CurrentUserId { get; set; }

    public Guid ReceiverId { get; set; }

    public required string Message { get; set; }
}
