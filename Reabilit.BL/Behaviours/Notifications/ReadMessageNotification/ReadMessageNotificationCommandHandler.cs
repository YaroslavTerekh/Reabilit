using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.ReadMessageNotification;

public class ReadMessageNotificationCommandHandler : IRequestHandler<ReadMessageNotificationCommand>
{
    private readonly DataContext _context;

    public ReadMessageNotificationCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(ReadMessageNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.MessageNotification
            .FirstOrDefaultAsync(mn => mn.Id == request.Id, cancellationToken);

        if (notification is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.Notification));
        }

        notification.IsRead = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
