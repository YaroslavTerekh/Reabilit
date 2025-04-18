using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.CancelEvent;

public class CancelEventCommand : IRequest
{
    public Guid ProcedureEventId { get; set; }

    [JsonIgnore]
    public Guid CurrentUserId { get; set; }
}
