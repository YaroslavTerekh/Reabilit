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

namespace Reabilit.BL.Behaviours.UserDoctor.GetMyTodaysEvents;

public class GetMyTodaysEventsQueryHandler : IRequestHandler<GetMyTodaysEventsQuery, List<ProcedureEventDTO>>
{
    private readonly DataContext _context;

    public GetMyTodaysEventsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProcedureEventDTO>> Handle(GetMyTodaysEventsQuery request, CancellationToken cancellationToken)
    {
        var doctorId = await _context.Doctors
            .Where(d => d.AppUserId == request.CurrentUsedId)
            .Select(d => d.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if(doctorId == Guid.Empty)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.ProcedureEvents
            .Where(pe => pe.DoctorId == doctorId && pe.StartsOn.Date == DateTime.UtcNow.Date)
            .Select(pe => new ProcedureEventDTO
            {
                Id = pe.Id,
                Title = pe.Title,
                Description = pe.Description,
                StartsOn = pe.StartsOn,
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
