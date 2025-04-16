using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class UserRoleDTO
{
    public Guid UserId { get; set; }

    public required string AppRole { get; set; }
}
