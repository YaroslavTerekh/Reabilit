using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.GetNewTreatmentNotifications;

public class GetNewTreatmentNotificationsQueryHandler : IRequestHandler<GetNewTreatmentNotificationsQuery, List<TreatmentNotification>>
{
    private readonly DataContext _context;

    public GetNewTreatmentNotificationsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<TreatmentNotification>> Handle(GetNewTreatmentNotificationsQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.TreatmentNotification
            .Where(pen => pen.AppUserId == request.CurrentUserId && !pen.IsRead)
            .ToListAsync(cancellationToken);
    }
}
