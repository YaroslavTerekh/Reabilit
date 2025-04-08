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

namespace Reabilit.BL.Behaviours.UserDoctor.GetDoctor;

public class GetDoctorQueryHandler : IRequestHandler<GetDoctorQuery, Doctor>
{
    private readonly DataContext _context;

    public GetDoctorQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Doctor> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorClass)
            .Include(d => d.AppUser)
            .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        return doctor;
    }
}
