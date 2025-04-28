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

namespace Reabilit.BL.Behaviours.UserDoctor.GetDoctors;

public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, List<DoctorDTO>>
{
    private readonly DataContext _context;

    public GetDoctorsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorDTO>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Doctors
            .Include(d => d.AppUser)
            .Include(d => d.DoctorClass)
            .AsQueryable();

        query = request.SearchText is null ? query :
            query.Where(d => d.AppUser!.FirstName.Contains(request.SearchText) ||
                        d.AppUser.LastName.Contains(request.SearchText) ||
                        d.DoctorClass!.ClassName.Contains(request.SearchText));

        return await query
            .Select(d => new DoctorDTO
            {
                Age = d.AppUser!.Age,
                AppUserId = d.AppUserId!,
                Biography = d.Biography!,
                Degree = d.Degree!,
                ExperienceInYear = d.ExperienceInYear!,
                FirstName = d.AppUser!.FirstName,
                LastName = d.AppUser!.LastName,
                Id = d.Id!,
                PhoneNumber = d.AppUser!.PhoneNumber!,
                DoctorClass = new DoctorClassDTO
                {
                    Id = d.DoctorClass!.Id,
                    ClassName = d.DoctorClass.ClassName
                },
                DoctorClassId = d.DoctorClassId!
            })
            .ToListAsync(cancellationToken);
    }
}
