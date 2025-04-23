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

namespace Reabilit.BL.Behaviours.UserDoctor.ModifyDoctorInfo;

public class ModifyDoctorInfoQueryHandler : IRequestHandler<ModifyDoctorInfoQuery, DoctorDTO>
{
    private readonly DataContext _context;

    public ModifyDoctorInfoQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<DoctorDTO> Handle(ModifyDoctorInfoQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorClass)
            .FirstOrDefaultAsync(d => d.AppUserId == request.CurrentUserId, cancellationToken);

        if (doctor is null)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == doctor.AppUserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Age = request.Age;
        user.PhoneNumber = request.PhoneNumber;
        doctor.ExperienceInYear = request.ExperienceInYear;
        doctor.Biography = request.Biography;
        doctor.Degree = request.Degree;

        await _context.SaveChangesAsync(cancellationToken);

        return new DoctorDTO
        {
            Age = doctor.AppUser!.Age,
            AppUserId = doctor.AppUserId,
            Biography = doctor.Biography,
            Degree = doctor.Degree,
            ExperienceInYear = doctor.ExperienceInYear,
            FirstName = doctor.AppUser.FirstName,
            LastName = doctor.AppUser.LastName,
            Id = doctor.Id,
            PhoneNumber = doctor.AppUser!.PhoneNumber!,
            DoctorClass = new DoctorClassDTO
            {
                Id = doctor.DoctorClass!.Id,
                ClassName = doctor.DoctorClass.ClassName
            },
            DoctorClassId = doctor.DoctorClassId
        };
    }
}
