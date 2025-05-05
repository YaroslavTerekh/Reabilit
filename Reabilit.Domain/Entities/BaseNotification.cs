using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class BaseNotification : BaseEntity
{
    public required string Message { get; set; }
    public bool IsRead { get; set; }

    public required Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
