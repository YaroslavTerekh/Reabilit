using MediatR;
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

namespace Reabilit.BL.Behaviours.UserPatient.GetPatient;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PatientDTO>
{
    private readonly DataContext _context;

    public GetPatientQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<PatientDTO> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Include(p => p.City)
            .Include(p => p.AppUser)
            .Include(p => p.Doctor)
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
                DoctorId = p.DoctorId
            })
            .FirstOrDefaultAsync(p => p.AppUserId == request.AppUserId, cancellationToken);

        if(patient is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        return patient;
    }
}
