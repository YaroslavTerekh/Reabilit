using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Authentication.RegisterPatient;

public class RegisterPatientCommand : IRequest
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string PhoneNumber { get; set; }

    public int Age { get; set; }

    public required string Password { get; set; }

    public Guid CityId { get; set; }

    public Guid AppUserId { get; set; }
}
