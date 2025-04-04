using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserDoctor.ModifyDoctorInfo;

public class ModifyDoctorInfoQueryHandler : IRequestHandler<ModifyDoctorInfoQuery, Doctor>
{
    private readonly DataContext _context;

    public ModifyDoctorInfoQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Doctor> Handle(ModifyDoctorInfoQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorClass)
            .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
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

        return doctor;
    }
}
