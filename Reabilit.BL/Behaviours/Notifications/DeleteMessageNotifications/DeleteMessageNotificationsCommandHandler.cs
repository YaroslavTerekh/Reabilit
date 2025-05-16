using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Behaviours.Notifications.DeleteEventNotifications;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.DeleteMessageNotifications;

public class DeleteMessageNotificationsCommandHandler : IRequestHandler<DeleteMessageNotificationsCommand>
{
    private readonly DataContext _context;

    public DeleteMessageNotificationsCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMessageNotificationsCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.MessageNotification
            .FirstOrDefaultAsync(mn => mn.Id == request.Id, cancellationToken);

        if (notification is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.Notification));
        }

        _context.MessageNotification.Remove(notification);
        await _context.SaveChangesAsync();
    }
}
