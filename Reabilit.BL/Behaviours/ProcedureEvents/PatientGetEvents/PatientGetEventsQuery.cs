using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.PatientGetEvents;

public class PatientGetEventsQuery : IRequest<List<ProcedureEvent>> // ToDo: ProceducreEvent DTO
{
    public Guid PatientId { get; set; }
}
