using MediatR;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.ModifyDoctorInfo;

public class ModifyDoctorInfoQuery : IRequest<DoctorDTO>
{
    [JsonIgnore]
    public Guid CurrentUserId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public int Age { get; set; }

    public string? Biography { get; set; }

    public required string Degree { get; set; }

    public int ExperienceInYear { get; set; }
}
