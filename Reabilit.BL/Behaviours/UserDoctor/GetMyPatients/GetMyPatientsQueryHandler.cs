using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.GetMyPatients;

public class GetMyPatientsQueryHandler : IRequestHandler<GetMyPatientsQuery, List<PatientDTO>>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public GetMyPatientsQueryHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<List<PatientDTO>> Handle(GetMyPatientsQuery request, CancellationToken cancellationToken)
    {
        var doctorId = await _context.Doctors
            .Where(d => d.AppUserId == request.CurrentUserId)
            .Select(d => d.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if(doctorId == Guid.Empty)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.Patients
            .Where(p => p.DoctorId == doctorId)
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
                Doctor = new DoctorDTO
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
                DoctorId = p.DoctorId,
                IsActive = p.IsActive,
                Analyzes = p.Analyzes.Select(pa => new AnalyzeDTO
                {
                    Id = pa.Id,
                    Title = pa.Title,
                    IsNormal = pa.IsNormal,
                    Unit = pa.Unit,
                    Value = pa.Value,
                    IconPath = _fileService.GetFullPathFromRoot(pa.IconPath),
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
