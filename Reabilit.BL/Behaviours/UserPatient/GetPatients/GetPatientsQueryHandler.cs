using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetPatients;

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, List<PatientDTO>>
{
    private readonly DataContext _context;

    public GetPatientsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<PatientDTO>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Patients
            .Include(p => p.AppUser)
            .Include(p => p.City)
            .Include(p => p.Doctor)
            .AsQueryable();

        query = request.SearchText is null ? query :
            query.Where(p => p.AppUser!.FirstName.Contains(request.SearchText) ||
                        p.AppUser.LastName.Contains(request.SearchText) ||
                        p.City!.CityName.Contains(request.SearchText));

        var result = await query.ToListAsync();

        return await query
            .Select(p => new PatientDTO
            {
                Id = p.Id,
                AppUserId = p.AppUserId,
                Age = p.AppUser!.Age,
                City = new CityDTO
                {
                    Id = p.CityId,
                    CityName = p.City!.CityName
                },
                Doctor = p.Doctor == null ? null : new DoctorDTO
                {
                    Age = p.Doctor!.AppUser!.Age,
                    AppUserId = p.Doctor.AppUserId,
                    Biography = p.Doctor.Biography,
                    Degree = p.Doctor.Degree,
                    ExperienceInYear = p.Doctor.ExperienceInYear,
                    FirstName = p.Doctor.AppUser.FirstName,
                    LastName = p.Doctor.AppUser.LastName,
                    Id = p.Doctor.Id,
                    PhoneNumber = p.Doctor!.AppUser!.PhoneNumber!,
                    DoctorClass = new DoctorClassDTO
                    {
                        Id = p.Doctor.DoctorClass!.Id,
                        ClassName = p.Doctor.DoctorClass.ClassName
                    },
                    DoctorClassId = p.Doctor.DoctorClassId
                },
                CityId = p.CityId,
                FirstName = p.AppUser.FirstName,
                LastName = p.AppUser.LastName,
                PhoneNumber = p.AppUser!.PhoneNumber!,
                DoctorId = p.DoctorId
            })
            .ToListAsync(cancellationToken);
    }
}
