using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetMyDoctor;

public class GetMyDoctorQueryHandler : IRequestHandler<GetMyDoctorQuery, DoctorDTO>
{
    private readonly DataContext _context;

    public GetMyDoctorQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<DoctorDTO> Handle(GetMyDoctorQuery request, CancellationToken cancellationToken)
    {
        var patientDoctor = await _context.Patients
            .Include(p => p.Doctor)
                .ThenInclude(pd => pd.DoctorClass)
            .Where(p => p.AppUserId == request.CurrentUserId)
            .Select(p => new DoctorDTO
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
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (patientDoctor is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        return patientDoctor;
    }
}
