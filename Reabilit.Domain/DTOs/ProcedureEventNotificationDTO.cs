using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class ProcedureEventNotificationDTO : BaseNotificationDTO
{
    public Guid? ProcedureEventId { get; set; }
    public ProcedureEvent? ProcedureEvent { get; set; }
}
