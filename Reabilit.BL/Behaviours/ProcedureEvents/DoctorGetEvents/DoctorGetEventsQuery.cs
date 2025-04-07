using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.DoctorGetEvents;

public class DoctorGetEventsQuery : IRequest<List<ProcedureEvent>>
{
    public Guid DoctorId { get; set; }
}
