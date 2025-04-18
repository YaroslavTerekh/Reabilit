using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.AddNewProcedureEvent;

public record AddNewProcedureEventCommand : IRequest
{
    public DateTime StartsOn { get; set; }


    [JsonIgnore]
    public Guid CurrentUserId { get; set; }
}
