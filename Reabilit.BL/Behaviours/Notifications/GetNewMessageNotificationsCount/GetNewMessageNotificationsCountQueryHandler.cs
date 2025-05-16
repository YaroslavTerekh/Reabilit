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

namespace Reabilit.BL.Behaviours.Notifications.GetNewMessageNotificationsCount;

public class GetNewMessageNotificationsCountQueryHandler : IRequestHandler<GetNewMessageNotificationsCountQuery, List<MessageNotificationDTO>>
{
    private readonly DataContext _context;

    public GetNewMessageNotificationsCountQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<MessageNotificationDTO>> Handle(GetNewMessageNotificationsCountQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.MessageNotification
            .Where(mn => mn.AppUserId == request.CurrentUserId && !mn.IsRead)
            .Include(mn => mn.ChatMessage)
            .Select(mn => new MessageNotificationDTO
            {
                Id = mn.Id,
                Message = mn.Message,
                IsRead = mn.IsRead,
                ChatMessageId = mn.ChatMessageId,
                ChatMessage = mn.ChatMessage
            })
            .ToListAsync(cancellationToken);
    }
}
