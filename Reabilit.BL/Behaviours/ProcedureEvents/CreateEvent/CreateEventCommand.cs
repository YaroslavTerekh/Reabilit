using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.CreateEvent;

public class CreateEventCommand : IRequest  // ToDo: Add field DateTime StatsOn
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime StartsOn { get; set; }
}
