using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.AddTreatmentRecommendation;

public class AddTreatmentRecommendationCommand : IRequest
{
    public Guid ProcedureEventId { get; set; }

    public Guid ReceiverId { get; set; }

    public required string Recommendation { get; set; }
}
