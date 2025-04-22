using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class PatientDTO : UserDTO
{
    public List<AnalyzeDTO> Analyzes { get; set; } = new();

    public Guid? DoctorId { get; set; }
    public DoctorDTO? Doctor { get; set; }

    public Guid CityId { get; set; }
    public CityDTO City { get; set; }

    public bool IsActive { get; set; }
}
