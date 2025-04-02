using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class Doctor : BaseEntity
{
    public int Age { get; set; }

    public List<Patient> Patients { get; set; } = new();

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
 