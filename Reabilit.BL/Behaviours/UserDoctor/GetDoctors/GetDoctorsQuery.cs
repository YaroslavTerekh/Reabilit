using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.GetDoctors;

public class GetDoctorsQuery : IRequest<List<Doctor>> // AddDoctorDTO
{
    public string? SearchText { get; set; }
}
