using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.AttachToDoctor;

public class AttachToDoctorCommandHandler : IRequestHandler<AttachToDoctorCommand>
{
    private readonly DataContext _context;

    public AttachToDoctorCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(AttachToDoctorCommand request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

        if(patient is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        if(!await _context.Doctors.AnyAsync(d => d.Id == request.DoctorId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        patient.DoctorId = request.DoctorId;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
