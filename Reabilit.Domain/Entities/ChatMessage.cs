using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class ChatMessage : BaseEntity
{
    public Guid SenderId { get; set; }
    public AppUser? Sender { get; set; }

    public Guid ReceiverId { get; set; }
    public AppUser? Receiver { get; set; }

    public required string MessageText { get; set; }

    public bool IsRead { get; set; }
}