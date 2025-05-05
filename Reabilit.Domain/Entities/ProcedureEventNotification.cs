using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class ProcedureEventNotification : BaseNotification
{
    public Guid? ProcedureEventId { get; set; }
    public ProcedureEvent? ProcedureEvent { get; set; }
}
