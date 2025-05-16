using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class ProcedureEvent : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Result { get; set; }

    public ProcedureEventStatus Status { get; set; } = ProcedureEventStatus.Planned;
    public DateTime StartsOn { get; set; }

    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public List<ProcedureEventNotification> EventsNotifications { get; set; } = new();
}

public enum ProcedureEventStatus
{
    Planned = 0,
    Cancelled = 1,
    Finished = 2
}