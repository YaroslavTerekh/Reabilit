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

namespace Reabilit.BL.Behaviours.ProcedureEvents.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
{
    private readonly DataContext _context;

    public CreateEventCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Patients.AnyAsync(p => p.Id == request.PatientId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        if (!await _context.Doctors.AnyAsync(d => d.Id == request.DoctorId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        var procedureEvent = new ProcedureEvent
        {
            Title = request.Title,
            Description = request.Description,
            DoctorId = request.DoctorId,
            PatientId = request.PatientId
        };

        await _context.ProcedureEvents.AddAsync(procedureEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

