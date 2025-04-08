using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class Doctor : BaseEntity
{
    public string? Biography { get; set; }
    public required string Degree { get; set; }
    public int ExperienceInYear { get; set; }

    public List<Patient> Patients { get; set; } = new();
    public List<ProcedureEvent> ProcedureEvents { get; set; } = new();
    public List<DoctorSchedule> DoctorSchedules { get; set; } = new();

    public Guid DoctorClassId { get; set; }
    public DoctorClass? DoctorClass { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
 