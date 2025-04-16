using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class DoctorDTO : UserDTO
{
    public string? Biography { get; set; }
    public required string Degree { get; set; }
    public int ExperienceInYear { get; set; }

    public Guid DoctorClassId { get; set; }
    public DoctorClassDTO? DoctorClass { get; set; }
}
