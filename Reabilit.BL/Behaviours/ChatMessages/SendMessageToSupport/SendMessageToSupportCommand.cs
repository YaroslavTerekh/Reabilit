using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.SendMessageToSupport;

public class SendMessageToSupportCommand : IRequest
{
    [JsonIgnore]
    public Guid CurrentUserId { get; set; }

    public required string Theme { get; set; }

    public required string Message { get; set; }
}
