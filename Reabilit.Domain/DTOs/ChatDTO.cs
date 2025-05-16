using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class ChatDTO 
{
    public Guid ReceiverId { get; set; }
    public AppUser? Receiver { get; set; }

    //public bool HasNewMessages { get; set; }
}
