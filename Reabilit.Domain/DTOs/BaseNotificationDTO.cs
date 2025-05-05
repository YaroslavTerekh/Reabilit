using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class BaseNotificationDTO
{
    public Guid Id { get; set; }

    public required string Message { get; set; }
    public bool IsRead { get; set; }
}
