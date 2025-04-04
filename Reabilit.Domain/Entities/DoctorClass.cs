using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class DoctorClass : BaseEntity
{
    public required string ClassName { get; set; }

    [JsonIgnore]
    public List<Doctor> Doctors { get; set; } = new();
}
