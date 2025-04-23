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

namespace Reabilit.BL.Behaviours.UserDoctor.GetMyInfo;

public class GetMyInfoQueryHandler : IRequestHandler<GetMyInfoQuery, DoctorDTO>
{
    private readonly DataContext _context;

    public GetMyInfoQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<DoctorDTO> Handle(GetMyInfoQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
                    .Include(d => d.DoctorClass)
                    .Include(d => d.AppUser)
                    .Select(p => new DoctorDTO
                    {
                        Age = p.AppUser!.Age,
                        AppUserId = p.AppUserId!,
                        Biography = p.Biography!,
                        Degree = p.Degree!,
                        ExperienceInYear = p.ExperienceInYear!,
                        FirstName = p.AppUser!.FirstName,
                        LastName = p.AppUser!.LastName,
                        Id = p.Id!,
                        PhoneNumber = p.AppUser!.PhoneNumber!,
                        DoctorClass = new DoctorClassDTO
                        {
                            Id = p.DoctorClass!.Id,
                            ClassName = p.DoctorClass.ClassName
                        },
                        DoctorClassId = p.DoctorClassId!
                    })
                    .FirstOrDefaultAsync(d => d.AppUserId == request.CurrentUserId, cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        return doctor;
    }
}
