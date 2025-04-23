using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class ProcedureEventDTO
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public ProcedureEventStatus Status { get; set; }
    public DateTime StartsOn { get; set; }
    public PatientDTO? Patient { get; set; }
    public DoctorDTO? Doctor { get; set; }
}
