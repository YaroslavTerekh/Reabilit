using MediatR;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.DoctorSchedules.GetDoctorFreeSlots;

public class GetDoctorFreeSlotsQuery : IRequest<List<FreeSlot>>
{
    public Guid DoctorId { get; set; }
}
