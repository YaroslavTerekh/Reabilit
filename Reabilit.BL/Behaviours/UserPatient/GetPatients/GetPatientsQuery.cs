using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetPatients;

public class GetPatientsQuery : IRequest<List<Patient>> // ToDo: Add PatientDTO
{
    public string? SearchText { get; set; }
}
