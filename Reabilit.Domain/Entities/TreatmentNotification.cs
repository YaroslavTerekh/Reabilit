using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class TreatmentNotification : BaseNotification
{
    public required string Recommendations { get; set; }
}
