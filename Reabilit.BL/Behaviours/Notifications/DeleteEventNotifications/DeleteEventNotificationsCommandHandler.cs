using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.DeleteEventNotifications;

public class DeleteEventNotificationsCommandHandler : IRequestHandler<DeleteEventNotificationsCommand>
{
    private readonly DataContext _context;

    public DeleteEventNotificationsCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteEventNotificationsCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.ProcedureEventNotification
            .FirstOrDefaultAsync(pen => pen.Id == request.Id, cancellationToken);

        if(notification is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.Notification));
        }

        _context.ProcedureEventNotification.Remove(notification);
        await _context.SaveChangesAsync();
    }
}
