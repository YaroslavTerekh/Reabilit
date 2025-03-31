using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class City : BaseEntity
{
    public required string CityName { get; set; }

    public List<Patient> Patients { get; set; } = new();
}
