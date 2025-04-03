using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Authentication.RegisterDoctor;

public class RegisterDoctorCommand : IRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public int Age { get; set; }

    public required string Password { get; set; }

    public required string Degree { get; set; }

    public int ExperienceInYear { get; set; }

    public Guid DoctorClassId { get; set; }
}
