using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.ToggleAccount;

public class ToggleAccountCommand : IRequest
{
    public Guid PatientId { get; set; }
}
