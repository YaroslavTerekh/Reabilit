using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class MessageDTO
{
    public Guid Id { get; set; }

    public Guid ReceiverId { get; set; }
    public required AppUser Receiver { get; set; }

    public Guid SenderId { get; set; }

    public required string MessageText { get; set; }

    public bool IsRead { get; set; }

    public bool IsSender { get; set; }

    public DateTime SentAt { get; set; }
}
