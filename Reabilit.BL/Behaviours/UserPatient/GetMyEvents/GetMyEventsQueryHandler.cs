using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetMyEvents;

public class GetMyEventsQueryHandler : IRequestHandler<GetMyEventsQuery, List<ProcedureEventDTO>>
{
    private readonly DataContext _context;

    public GetMyEventsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProcedureEventDTO>> Handle(GetMyEventsQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.AppUserId == request.CurrentUserId, cancellationToken);

        if(patient is null)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.ProcedureEvents
            .Include(pe => pe.Doctor)
            .OrderByDescending(pe => pe.CreatedDate)
            .Where(pe => pe.PatientId == patient.Id)
            .Select(pe => new ProcedureEventDTO
            {
                Id = pe.Id,
                Title = pe.Title,
                Description = pe.Description,
                Result = pe.Result,
                StartsOn = pe.StartsOn,
                Status = pe.Status,
                Doctor = new DoctorDTO
                {
                    Age = pe.Doctor!.AppUser!.Age,
                    AppUserId = pe.Doctor.AppUserId,
                    Biography = pe.Doctor.Biography,
                    Degree = pe.Doctor.Degree,
                    ExperienceInYear = pe.Doctor.ExperienceInYear,
                    FirstName = pe.Doctor.AppUser.FirstName,
                    LastName = pe.Doctor.AppUser.LastName,
                    Id = pe.Doctor.Id,
                    PhoneNumber = pe.Doctor!.AppUser!.PhoneNumber!,
                    DoctorClass = new DoctorClassDTO
                    {
                        Id = pe.Doctor.DoctorClass!.Id,
                        ClassName = pe.Doctor.DoctorClass.ClassName
                    },
                    DoctorClassId = pe.Doctor.DoctorClassId
                },
                Patient = null
            })
            .ToListAsync(cancellationToken);
    }
}
