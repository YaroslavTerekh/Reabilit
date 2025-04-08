using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.GetDoctor;

public class GetDoctorQuery : IRequest<Doctor> // ToDo: Add DoctorDTO
{
    public Guid DoctorId { get; set; }
}
