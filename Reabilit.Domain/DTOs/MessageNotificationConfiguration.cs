using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class MessageNotificationConfiguration
{
    public required string Message { get; set; }
    public Guid ChatMessageId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
}
