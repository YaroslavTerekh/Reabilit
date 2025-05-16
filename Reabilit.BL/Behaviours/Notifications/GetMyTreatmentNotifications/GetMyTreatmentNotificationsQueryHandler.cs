using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.GetMyTreatmentNotifications;

public class GetMyTreatmentNotificationsQueryHandler : IRequestHandler<GetMyTreatmentNotificationsQuery, List<TreatmentNotification>>
{
    private readonly DataContext _context;

    public GetMyTreatmentNotificationsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<TreatmentNotification>> Handle(GetMyTreatmentNotificationsQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.TreatmentNotification
            .Where(tn => tn.AppUserId == request.CurrentUserId)
            .OrderByDescending(tn => tn.CreatedDate)
            .ToListAsync(cancellationToken);
    }
}
