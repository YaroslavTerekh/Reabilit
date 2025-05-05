using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Realizations;

public class EventNotificationServiceHelper
{
    private readonly DataContext _context;
    private readonly IEventNotificationService _eventNotificationService;

    public EventNotificationServiceHelper(DataContext context, IEventNotificationService eventNotificationService)
    {
        _context = context;
        _eventNotificationService = eventNotificationService;
    }

    public async Task PatientCreateAndSendEventNotificationAsync(Guid userId, Guid eventId, string message, CancellationToken cancellationToken = default)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.AppUserId == userId, cancellationToken);

        if(patient is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        var procedureEvent = await _context.ProcedureEvents.FirstOrDefaultAsync(pe => pe.Id == eventId, cancellationToken);

        if(procedureEvent is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

    }
}
