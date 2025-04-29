using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reabilit.Domain.Entities;
using System.Text.Json.Serialization;
using Reabilit.Domain.DTOs;

namespace Reabilit.BL.Behaviours.UserPatient.ModifyPatientInfo;

public class ModifyPatientInfoQuery : IRequest<PatientDTO> 
{
    [JsonIgnore]
    public Guid CurrentUserId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public int Age { get; set; }

    public Guid CityId { get; set; }
}
