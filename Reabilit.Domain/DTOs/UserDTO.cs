using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }

    public Guid AppUserId { get; set; }

    public required string PhoneNumber { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public int Age { get; set; }
}
