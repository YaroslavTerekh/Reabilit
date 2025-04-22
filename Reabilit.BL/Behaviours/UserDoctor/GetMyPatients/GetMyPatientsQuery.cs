using MediatR;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.GetMyPatients;

public record GetMyPatientsQuery(Guid CurrentUserId) : IRequest<List<PatientDTO>>;
