using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.CancelEvent;

public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand>
{
    private readonly DataContext _context;

    public CancelEventCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        var patientId = await _context.Patients
            .Where(p => p.AppUserId == request.CurrentUserId)
            .Select(p => p.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if(patientId == Guid.Empty)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        var procedureEvent = await _context.ProcedureEvents
            .FirstOrDefaultAsync(pe => pe.Id == request.ProcedureEventId, cancellationToken);

        if(procedureEvent is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

        if (procedureEvent.PatientId != patientId)
        {
            throw new AuthException(StatusCodes.Status403Forbidden, ErrorMessages.Unauthorized403);
        }

        procedureEvent.Status = ProcedureEventStatus.Cancelled;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
