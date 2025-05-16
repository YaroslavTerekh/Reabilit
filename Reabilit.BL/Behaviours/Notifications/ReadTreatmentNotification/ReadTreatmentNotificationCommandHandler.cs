using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.ReadTreatmentNotification;

public class ReadTreatmentNotificationCommandHandler : IRequestHandler<ReadTreatmentNotificationCommand>
{
    private readonly DataContext _context;

    public ReadTreatmentNotificationCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(ReadTreatmentNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.TreatmentNotification
            .FirstOrDefaultAsync(mn => mn.Id == request.Id, cancellationToken);

        if (notification is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.Notification));
        }

        notification.IsRead = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
