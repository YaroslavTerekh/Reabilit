using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using Reabilit.Domain.SignalrHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Realizations;

public class EventNotificationService : IEventNotificationService
{
    private readonly DataContext _context;
    private readonly IHubContext<NotificationsHub> _hubContext;

    public EventNotificationService(DataContext context, IHubContext<NotificationsHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public Task DeleteNotificationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAndSendEventNotificationAsync(EventNotificationConfiguration configuration, CancellationToken cancellationToken = default)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == configuration.AppUserId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.User));
        }

        if (!await _context.ProcedureEvents.AnyAsync(pe => pe.Id == configuration.ProcedureEventId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

        var eventNotification = new ProcedureEventNotification
        {
            IsRead = false,
            Message = configuration.Message,
            AppUserId = configuration.AppUserId,
            ProcedureEventId = configuration.ProcedureEventId,
        };

        try
        {
            await _context.ProcedureEventNotification.AddAsync(eventNotification, cancellationToken);

            await _hubContext.Clients.User(eventNotification.AppUserId.ToString()).SendAsync(nameof(CreateAndSendEventNotificationAsync), eventNotification);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch { } // ToDo: Create block for catching

    }
}
