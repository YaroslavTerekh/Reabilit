using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.ReadAllNotifications;

public class ReadAllNotificationsCommandHandler : IRequestHandler<ReadAllNotificationsCommand>
{
    private readonly DataContext _context;

    public ReadAllNotificationsCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(ReadAllNotificationsCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        var eventNotifications = await _context.ProcedureEventNotification
            .Where(pen => pen.AppUserId == request.CurrentUserId)
            .ToListAsync(cancellationToken);

        foreach (var notification in eventNotifications)
        {
            notification.IsRead = true;
        }

        var messageNotifications = await _context.MessageNotification
            .Where(pen => pen.AppUserId == request.CurrentUserId)
            .ToListAsync(cancellationToken);

        foreach (var notification in messageNotifications)
        {
            notification.IsRead = true;
        }

        var treatmentNotifications = await _context.TreatmentNotification
            .Where(pen => pen.AppUserId == request.CurrentUserId)
            .ToListAsync(cancellationToken);

        foreach (var notification in treatmentNotifications)
        {
            notification.IsRead = true;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
