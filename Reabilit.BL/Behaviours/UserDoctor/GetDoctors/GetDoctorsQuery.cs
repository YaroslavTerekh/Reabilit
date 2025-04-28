using MediatR;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.GetDoctors;

public class GetDoctorsQuery : IRequest<List<DoctorDTO>>
{
    public string? SearchText { get; set; }
}
