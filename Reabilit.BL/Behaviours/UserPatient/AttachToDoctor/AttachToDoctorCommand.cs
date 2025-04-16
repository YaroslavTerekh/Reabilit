using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.AttachToDoctor;

public class AttachToDoctorCommand : IRequest
{
    public Guid DoctorId { get; set; }
    
    public Guid PatientId { get; set; }
}
