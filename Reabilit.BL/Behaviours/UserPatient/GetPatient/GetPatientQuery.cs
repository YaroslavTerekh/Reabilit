using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetPatient;

public class GetPatientQuery : IRequest<Patient> //ToDo: Add PatientDTO
{
    public Guid PatientId { get; set; }
}
