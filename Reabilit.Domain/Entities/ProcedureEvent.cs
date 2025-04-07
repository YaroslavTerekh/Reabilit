using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class ProcedureEvent : BaseEntity //ToDo: add event time
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public ProcedureEventStatus Status { get; set; } = ProcedureEventStatus.Planned;

    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
}

public enum ProcedureEventStatus
{
    Planned = 0,
    Cancelled = 1,
    Finished = 2
}