using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.GetMyEvents;

public class GetMyEventsQueryHandler : IRequestHandler<GetMyEventsQuery, List<ProcedureEventDTO>>
{
    private readonly DataContext _context;

    public GetMyEventsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProcedureEventDTO>> Handle(GetMyEventsQuery request, CancellationToken cancellationToken)
    {
        var doctorId = await _context.Doctors
            .Where(d => d.AppUserId == request.CurrentUserId)
            .Select(d => d.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if(doctorId == Guid.Empty)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.ProcedureEvents
            .Where(pe => pe.DoctorId == doctorId)
            .OrderByDescending(pe => pe.CreatedDate)
            .Select(pe => new ProcedureEventDTO
            {
                Id = pe.Id,
                Title = pe.Title,
                Description = pe.Description,
                StartsOn = pe.StartsOn,
                Result = pe.Result,
                Status = pe.Status,
                Doctor = null,
                Patient = new PatientDTO
                {
                    Id = pe!.Patient!.Id,
                    AppUserId = pe!.Patient!.AppUserId,
                    Age = pe!.Patient!.AppUser!.Age,
                    City = new CityDTO
                    {
                        Id = pe!.Patient!.CityId,
                        CityName = pe!.Patient!.City!.CityName
                    },
                    Doctor = null,
                    CityId = pe!.Patient!.CityId,
                    FirstName = pe!.Patient!.AppUser.FirstName,
                    LastName = pe!.Patient!.AppUser.LastName,
                    PhoneNumber = pe!.Patient!.AppUser!.PhoneNumber!,
                    DoctorId = pe!.Patient!.DoctorId,
                    IsActive = pe!.Patient!.IsActive,
                    Analyzes = new()
                }
            })
            .ToListAsync(cancellationToken);
    }
}
