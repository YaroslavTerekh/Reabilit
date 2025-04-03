using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class DoctorClass : BaseEntity
{
    public required string ClassName { get; set; }

    public List<Doctor> Doctors { get; set; } = new();
}
