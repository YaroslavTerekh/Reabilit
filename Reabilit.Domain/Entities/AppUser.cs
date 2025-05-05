using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class AppUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public int Age { get; set; }

    public List<ProcedureEventNotification> EventsNotifications { get; set; } = new();
}
