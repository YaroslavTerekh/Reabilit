using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.DeleteTreatmentNotification;

public class DeleteTreatmentNotificationCommandHandler : IRequestHandler<DeleteTreatmentNotificationCommand>
{
    private readonly DataContext _context;

    public DeleteTreatmentNotificationCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTreatmentNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.TreatmentNotification
            .FirstOrDefaultAsync(mn => mn.Id == request.Id, cancellationToken);

        if (notification is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.Notification));
        }

        _context.TreatmentNotification.Remove(notification);
        await _context.SaveChangesAsync();
    }
}
