using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.GetMyNotifications;

public class GetMyEventNotificationsQueryHandler : IRequestHandler<GetMyEventNotificationsQuery, List<ProcedureEventNotificationDTO>>
{
    private readonly DataContext _context;

    public GetMyEventNotificationsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProcedureEventNotificationDTO>> Handle(GetMyEventNotificationsQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.ProcedureEventNotification
            .Where(pen => pen.AppUserId == request.CurrentUserId)
            .Include(pen => pen.ProcedureEvent)
            .Select(pen => new ProcedureEventNotificationDTO
            {
                Id = pen.Id,
                Message = pen.Message,
                ProcedureEvent = pen.ProcedureEvent,
                IsRead = pen.IsRead,
                ProcedureEventId = pen.ProcedureEventId
            })
            .ToListAsync(cancellationToken);
    }
}
