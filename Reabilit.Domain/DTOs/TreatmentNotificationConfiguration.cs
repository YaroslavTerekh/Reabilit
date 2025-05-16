using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class TreatmentNotificationConfiguration
{
    public required string Recommendations { get; set; }
    public required string Message { get; set; }
    public Guid AppUserId { get; set; }
    public Guid ProcedureEventId { get; set; }
}
