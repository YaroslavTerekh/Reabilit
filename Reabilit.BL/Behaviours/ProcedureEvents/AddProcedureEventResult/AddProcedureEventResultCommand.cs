using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.AddProcedureEventResult;

public class AddProcedureEventResultCommand : IRequest
{
    public required string Result { get; set; }

    public Guid ProcedureEventId { get; set; }
}
