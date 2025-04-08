using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.EditEvent;

public class EditEventCommand : IRequest
{
    public Guid EventId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public ProcedureEventStatus Status { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
}
