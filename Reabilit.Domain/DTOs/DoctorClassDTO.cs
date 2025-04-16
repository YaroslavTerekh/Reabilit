using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class DoctorClassDTO
{
    public Guid Id { get; set; }
    public required string ClassName { get; set; }
}
