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

namespace Reabilit.BL.Behaviours.UserPatient.ModifyPatientInfo;

public class ModifyPatientInfoQueryHandler : IRequestHandler<ModifyPatientInfoQuery, PatientDTO>
{
    private readonly DataContext _context;

    public ModifyPatientInfoQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<PatientDTO> Handle(ModifyPatientInfoQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Include(p => p.AppUser)
            .Include(p => p.City)
            .Include(p => p.Doctor)
                .ThenInclude(pd => pd.DoctorClass)
            .Include(p => p.Doctor)
                .ThenInclude(pd => pd.AppUser)
            .FirstOrDefaultAsync(p => p.AppUserId == request.CurrentUserId, cancellationToken);
    
        if(patient is null)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == patient.AppUserId, cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        if((request.PhoneNumber != user.PhoneNumber) && await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber))
        {
            throw new RequestException(ErrorMessages.PhoneNumberExists);
        }

        var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == request.CityId, cancellationToken);

        if(city is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.City));
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Age = request.Age;
        user.PhoneNumber = request.PhoneNumber;
        patient.CityId = request.CityId;

        await _context.SaveChangesAsync(cancellationToken);

        return new PatientDTO
        {
            Id = patient.Id,
            AppUserId = patient.AppUserId,
            Age = patient.AppUser!.Age,
            City = new CityDTO
            {
                Id = patient.CityId,
                CityName = patient.City!.CityName
            },
            Doctor = new DoctorDTO
            {
                Age = patient.Doctor!.AppUser!.Age,
                AppUserId = patient.Doctor.AppUserId,
                Biography = patient.Doctor.Biography,
                Degree = patient.Doctor.Degree,
                ExperienceInYear = patient.Doctor.ExperienceInYear,
                FirstName = patient.Doctor.AppUser.FirstName,
                LastName = patient.Doctor.AppUser.LastName,
                Id = patient.Doctor.Id,
                PhoneNumber = patient.Doctor!.AppUser!.PhoneNumber!,
                DoctorClass = new DoctorClassDTO
                {
                    Id = patient.Doctor.DoctorClass!.Id,
                    ClassName = patient.Doctor.DoctorClass.ClassName
                },
                DoctorClassId = patient.Doctor.DoctorClassId
            },
            CityId = patient.CityId,
            FirstName = patient.AppUser.FirstName,
            LastName = patient.AppUser.LastName,
            PhoneNumber = patient.AppUser!.PhoneNumber!,
            DoctorId = patient.DoctorId
        };
    }
}
